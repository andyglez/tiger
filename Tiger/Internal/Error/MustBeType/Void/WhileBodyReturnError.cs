using Antlr.Runtime.Tree;

namespace Tiger
{
    public class WhileBodyReturnError : MustBeVoidError
    {
        public WhileBodyReturnError(ITree tree, string type_name) : base(tree, "Body of a 'while' statement return type", type_name) { }

    }
}
