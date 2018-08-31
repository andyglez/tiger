using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class MustBeIntError : Error
    {
        public MustBeIntError(ITree tree, string expr, string return_type) 
            : base(tree, "{0} must be of type 'int' instead is returning {1}", expr, return_type) { }
    }
}
