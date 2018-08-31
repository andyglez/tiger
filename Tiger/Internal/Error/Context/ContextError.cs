using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class ContextError : Error
    {
        public ContextError(ITree tree, string message) : base(tree, "{0} in the current context", message) { }
    }
}
