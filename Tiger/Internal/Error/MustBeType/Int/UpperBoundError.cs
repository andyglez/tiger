using Antlr.Runtime.Tree;

namespace Tiger
{
    public class UpperBoundError : MustBeIntError
    {
        public UpperBoundError(ITree tree, string return_type) : base(tree, "Upper bound on a 'for' statement return type", return_type) { }

    }
}
