using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class MustBeVoidError : Error
    {
        public MustBeVoidError(ITree tree, string statement, string return_type) 
            : base(tree, "{0} must be of type 'void' instead is returning {1}", statement, return_type) { }
    }
}
