using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionNotFoundError : NotFoundError
    {
        public FunctionNotFoundError(ITree tree, string identifier) : base(tree, string.Format("Function '{0}'", identifier)) { }
    }
}
