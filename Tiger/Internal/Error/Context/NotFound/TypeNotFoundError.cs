using Antlr.Runtime.Tree;

namespace Tiger
{
    public class TypeNotFoundError : NotFoundError
    {
        public TypeNotFoundError(ITree tree, string identifier) : base(tree, string.Format("Type '{0}'", identifier)) { }
    }
}
