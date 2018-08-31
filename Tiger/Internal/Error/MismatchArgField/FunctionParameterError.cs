using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionParameterError : ArgFieldError
    {
        public FunctionParameterError(ITree tree, string function_name, int index, string arg_type)
            : base(tree, string.Format("Calling {0} arg with index {1} must return {2} and", function_name, index, arg_type)) { }
    }
}
