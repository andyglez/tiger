using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class MismatchReturnError : Error
    {
        public MismatchReturnError(ITree tree, string id, string declaration_return_type, string expression_return_type)
            : base(tree, "{0} returns '{1}' and it doesn't match with the right side return type '{2}'", id, declaration_return_type, expression_return_type) { }
    }
}
