using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionAlreadyFoundError : AlreadyFoundError
    {
        public FunctionAlreadyFoundError(ITree tree, string identifier) : base(tree, string.Format("Function '{0}'", identifier)) { }
    }
}
