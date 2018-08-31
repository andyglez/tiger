using Antlr.Runtime.Tree;

namespace Tiger
{
    public class InferError : Error
    {
        public InferError(ITree tree) : base(tree, "No variable can't be defined 'void' or 'nil' as their type") { }
    }
}
