using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Specifies the variables declarations sequence
    /// </summary>
    public class VarDeclarationSequenceNode : DeclarationSequenceNode
    {
        private List<VariableDeclarationNode> declarations;
        /// <summary>
        /// The list that contains all declared functions in the sequence
        /// </summary>
        public List<VariableDeclarationNode> DeclaredVars
        {
            get
            {
                if (declarations == null)
                {
                    declarations = new List<VariableDeclarationNode>();
                    for (int i = 0; i < ChildCount; i++)
                        declarations.Add(GetChild(i) as VariableDeclarationNode);
                }
                return declarations;
            }
        }
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            foreach (var variables in DeclaredVars)
                variables.CheckSemantics(scope, errors);
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (var variable in DeclaredVars)
                variable.GenerateCode(code_generator, type_builder, module_builder);
        }

        public void RestoreVariable(ILGenerator code_generator)
        {
            foreach (var variable in DeclaredVars)
                if (variable is LetVarDeclarationNode)
                    (variable as LetVarDeclarationNode).RestoreVariable(code_generator);
        }
    }
}
