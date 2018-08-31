using Antlr.Runtime.Tree;

namespace Tiger
{
    public class TypeAlreadyDefinedStdLibError : AlreadyDefinedStdLibError
    {
        public TypeAlreadyDefinedStdLibError(ITree tree, string identifier) : base(tree, string.Format("Type {0}", identifier)) { }
    }
}
