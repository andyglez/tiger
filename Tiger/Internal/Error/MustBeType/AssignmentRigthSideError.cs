using Antlr.Runtime.Tree;

namespace Tiger
{
    public class AssignmentRigthSideError : Error
    {
        public AssignmentRigthSideError(ITree tree) : base(tree, "Right side of an assignment statement must return a value") { }
    }
}
