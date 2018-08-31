using Antlr.Runtime.Tree;

namespace Tiger
{
    public class ArgCountError : ArgFieldError
    {
        public ArgCountError(ITree tree, string function_name, int param_count)
            : base(tree, string.Format("Calling {0} must have {1} args and", function_name, param_count)) { }
    }
}
