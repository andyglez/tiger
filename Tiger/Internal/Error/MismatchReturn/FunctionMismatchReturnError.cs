using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionMismatchReturnError : MismatchReturnError
    {
        public FunctionMismatchReturnError(ITree tree, string id, string declaration_return_type, string expression_return_type)
            : base(tree, string.Format("Function '{0}'", id), declaration_return_type, expression_return_type) { }
    }
}
