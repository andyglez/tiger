using Antlr.Runtime.Tree;

namespace Tiger
{
    public class IfThenElseMismatchError : Error
    {
        public IfThenElseMismatchError(ITree tree, string then_return_type, string else_return_type) : 
            base(tree, "Expressions 'then' and 'else' returns {0} and {1} respectively so they doesn't match", then_return_type, else_return_type) { }
    }
}
