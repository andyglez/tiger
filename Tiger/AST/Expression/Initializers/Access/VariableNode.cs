using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents when a variable value is to be retrieved
    /// </summary>
    public class VariableNode : LValueNode
    {
        /// <summary>
        /// Gets the name of the variable
        /// </summary>
        public string Name { get { return GetChild(0).Text; } }
        /// <summary>
        /// Gets or sets the declaration of this variable
        /// </summary>
        public VariableDeclarationNode VarDeclaration { get; set; }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            VarDeclaration = scope.GetVariable(Name);
            ReturnType = BadType.GetInstance;
            if (VarDeclaration == null && !(VarDeclaration is VariableDeclarationNode))
                errors.Add(new VariableNotFoundError(GetChild(0), Name));
            else
                ReturnType = VarDeclaration.TypeDeclared;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            VarDeclaration.GetValue(code_generator);
        }
        public override void SetAssingCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder, ExpressionNode assingnation)
        {
            var stdVarDecl = VarDeclaration as LetVarDeclarationNode;
            if (stdVarDecl != null)
            {
                assingnation.GenerateCode(code_generator, type_builder, module_builder);
                code_generator.Emit(OpCodes.Stsfld, stdVarDecl.VariableBuilder);
                return;
            }
            var argVarDecl = (FunctionParameterNode)VarDeclaration;
            assingnation.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Starg, argVarDecl.Index);
        }
    }
}
