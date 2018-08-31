using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class AlreadyFoundError : ContextError
    {
        public AlreadyFoundError(ITree tree, string identifier) : base(tree, string.Format("{0} already exists", identifier)) { }
    }
}
