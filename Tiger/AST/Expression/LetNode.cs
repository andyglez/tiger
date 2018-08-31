using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the let-in-end statement
    /// </summary>
    public class LetNode : ExpressionNode
    {
        /// <summary>
        /// Gets the in-statement (contains a list of a expressions)
        /// </summary>
        public ExpressionListNode ExpressionList { get { return Children[1] as ExpressionListNode; } }

        private List<DeclarationSequenceNode> declarations;
        /// <summary>
        /// Gets the list that contains all of the declarations sequences
        /// </summary>
        public List<DeclarationSequenceNode> DeclarationList
        {
            get
            {
                if (declarations == null)
                {
                    declarations = new List<DeclarationSequenceNode>();
                    for (int i = 0; i < Children[0].ChildCount; i++)
                        declarations.Add(Children[0].GetChild(i) as DeclarationSequenceNode);
                }
                return declarations;
            }
        }


        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            var child_scope = new Scope(scope);
            foreach (var declaration in DeclarationList)
                declaration.CheckSemantics(child_scope, errors);
            ExpressionList.CheckSemantics(child_scope, errors);
            if (ExpressionList.ReturnType is ArrayType)
            {
                if (scope.GetType(((ArrayType)ExpressionList.ReturnType).Name) == null)
                    errors.Add(new TypeNotFoundError(ExpressionList, ExpressionList.ReturnType.Id));
            }
            else if (!(ExpressionList.ReturnType is NilType) && scope.GetType(ExpressionList.ReturnType.Id) == null && !ExpressionList.ReturnType.Equals(VoidType.GetInstance))
                errors.Add(new TypeNotFoundError(ExpressionList, ExpressionList.ReturnType.Id));
            ReturnType = ExpressionList.ReturnType;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (var declaration in DeclarationList)
                declaration.GenerateCode(code_generator, type_builder, module_builder);

            ExpressionList.GenerateCode(code_generator, type_builder, module_builder);

            foreach (var declaration in DeclarationList)
                if (declaration is VarDeclarationSequenceNode)
                    ((VarDeclarationSequenceNode)declaration).RestoreVariable(code_generator);
        }
    }
}
