using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class NotFoundError : ContextError
    {
        public NotFoundError(ITree tree, string identifier) : base(tree, string.Format("{0} doesn't exists", identifier)) { }
    }
}
