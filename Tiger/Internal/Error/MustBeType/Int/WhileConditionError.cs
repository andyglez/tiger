using Antlr.Runtime.Tree;

namespace Tiger
{
    public class WhileConditionError : MustBeIntError
    {
        public WhileConditionError(ITree tree, string return_type) : base(tree, "While condition statement return type", return_type) { }
    }
}
