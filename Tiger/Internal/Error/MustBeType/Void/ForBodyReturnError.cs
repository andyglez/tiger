using Antlr.Runtime.Tree;

namespace Tiger
{
    public class ForBodyReturnError : MustBeVoidError
    {
        public ForBodyReturnError(ITree tree, string type_name) : base(tree, "Body of a 'for' statement return type", type_name) { }
    }
}
