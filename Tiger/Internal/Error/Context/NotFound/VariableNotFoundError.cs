using Antlr.Runtime.Tree;

namespace Tiger
{
    public class VariableNotFoundError : NotFoundError
    {
        public VariableNotFoundError(ITree tree, string identifier) : base(tree, string.Format("Variable '{0}'", identifier)) { }
    }
}
