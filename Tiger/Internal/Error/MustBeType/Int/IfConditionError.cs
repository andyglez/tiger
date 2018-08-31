using Antlr.Runtime.Tree;

namespace Tiger
{
    public class IfConditionError : MustBeIntError
    {
        public IfConditionError(ITree tree, string return_type) : base(tree, "If condition statement return type", return_type) { }
    }
}
