using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionArgAlreadyFoundError : AlreadyFoundError
    {
        public FunctionArgAlreadyFoundError(ITree tree, string identifier) : base(tree, string.Format("Function argument '{0}'", identifier)) { }

    }
}
