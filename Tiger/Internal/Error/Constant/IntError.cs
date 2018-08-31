using Antlr.Runtime.Tree;

namespace Tiger
{
    public class IntError : Error
    {
        public IntError(ITree tree, string expression) : base(tree, "Expression {0} can'be parsed into 'int'", expression) { }
    }
}
