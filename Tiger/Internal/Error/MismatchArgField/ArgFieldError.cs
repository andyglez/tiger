using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class ArgFieldError : Error
    {
        public ArgFieldError(ITree tree, string message) : base(tree, "{0} doesn't match", message) { }
    }
}
