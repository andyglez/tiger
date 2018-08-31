using Antlr.Runtime.Tree;

namespace Tiger
{
    public class ForReadOnlyError : Error
    {
        public ForReadOnlyError(ITree tree, string variable_name) : base(tree, "Variable '{0}' is the indexer of her 'for' loop, it is read-only", variable_name) { }
    }
}
