using Antlr.Runtime.Tree;

namespace Tiger
{
    public class VariableAlreadyFoundError : AlreadyFoundError
    {
        public VariableAlreadyFoundError(ITree tree, string identifier) : base(tree, string.Format("Variable '{0}'", identifier)) { }
    }
}
