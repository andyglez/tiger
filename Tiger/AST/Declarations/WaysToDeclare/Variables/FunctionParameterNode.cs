using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides a variable declaration on functions as an argument
    /// </summary>
    public class FunctionParameterNode : VariableDeclarationNode
    {
        /// <summary>
        /// Gets or sets the index in the parent function
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Gets the type of the variable
        /// </summary>
        public string TypeName { get { return Children[1].Text; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            if (scope.GetVariable(Identifier) != null)
                errors.Add(new FunctionArgAlreadyFoundError(this, Identifier));
            else
                scope.VariableDictionary.Add(Identifier, this);
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            VariableBuilder = type_builder.DefineField(Identifier, TypeDeclared.BaseType, FieldAttributes.Static | FieldAttributes.Public);
            LocalBuilder = code_generator.DeclareLocal(VariableBuilder.FieldType);

            code_generator.Emit(OpCodes.Ldsfld, VariableBuilder);
            code_generator.Emit(OpCodes.Stloc, LocalBuilder);
            code_generator.Emit(OpCodes.Ldarg, Index);
            code_generator.Emit(OpCodes.Stsfld, VariableBuilder);
        }

        public void RestoreVariable(ILGenerator code_generator)
        {
            code_generator.Emit(OpCodes.Ldloc, LocalBuilder);
            code_generator.Emit(OpCodes.Stsfld, VariableBuilder);
        }

        public override void GetValue(ILGenerator code_generator)
        {
            code_generator.Emit(OpCodes.Ldsfld, VariableBuilder);
        }
    }
}
