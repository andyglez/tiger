using Antlr.Runtime.Tree;

namespace Tiger
{
    public class TypeAlreadyFoundError : AlreadyFoundError
    {
        public TypeAlreadyFoundError(ITree tree, string identifier) : base(tree, string.Format("Type '{0}'", identifier)) { }
    }
}
