using Antlr.Runtime.Tree;

namespace Tiger
{
    class AssignmentMismatchError : MismatchReturnError
    {
        public AssignmentMismatchError(ITree tree, string left_side, string right_side)
            : base(tree, "Assignment left side", left_side, right_side) { }
    }
}
