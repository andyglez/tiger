using Antlr.Runtime.Tree;

namespace Tiger
{
    public class MustBeArrayError : Error
    {
        public MustBeArrayError(ITree tree, string return_type)
            : base(tree, "Operators [] can only be applied to elements of type 'array' instead expression is of type {0}", return_type) { }
    }
}
