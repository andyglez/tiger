using Antlr.Runtime.Tree;

namespace Tiger
{
    public class StringError : Error
    {
        public StringError(ITree tree) : base(tree, "Wrong string sequence") { }
    }
}
