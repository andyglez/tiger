using Antlr.Runtime.Tree;

namespace Tiger
{
    public class VariableMismatchReturnError : MismatchReturnError
    {
        public VariableMismatchReturnError(ITree tree, string id, string declaration_return_type, string expression_return_type)
            : base(tree, string.Format("Variable '{0}' declaration", id), declaration_return_type, expression_return_type) { }
    }
}
