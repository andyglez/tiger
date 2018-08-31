using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides a variable declaration on for loops of the language
    /// </summary>
    public class ForVarDeclarationNode : VariableDeclarationNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            InitializeVar.CheckSemantics(scope.Parent, errors);
            //Check if the variable is correctly initialized as Int for indexing
            if (!InitializeVar.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new ForVariableMismatchReturnError(InitializeVar, Identifier, InitializeVar.ReturnType.Id));
            TypeDeclared = IntType.GetInstance;
            scope.VariableDictionary.Add(Identifier, this);
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            VariableBuilder = type_builder.DefineField(Identifier, TypeDeclared.BaseType, FieldAttributes.Static | FieldAttributes.Public);
            LocalBuilder = code_generator.DeclareLocal(typeof(int));

            code_generator.Emit(OpCodes.Ldsfld, VariableBuilder);
            code_generator.Emit(OpCodes.Stloc, LocalBuilder);
            InitializeVar.GenerateCode(code_generator, type_builder, module_builder);
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
