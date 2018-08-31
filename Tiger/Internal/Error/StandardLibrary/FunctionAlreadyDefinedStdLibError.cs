using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FunctionAlreadyDefinedStdLibError : AlreadyDefinedStdLibError
    {
        public FunctionAlreadyDefinedStdLibError(ITree tree, string identifier) : base(tree, string.Format("Function {0}", identifier)) { }
    }
}
