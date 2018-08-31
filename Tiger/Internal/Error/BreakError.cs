using Antlr.Runtime.Tree;

namespace Tiger
{
    public class BreakError : Error
    {
        public BreakError(ITree tree) : base(tree, "Break statement must be within the body of a 'while' or 'for' statement") { }
    }
}
