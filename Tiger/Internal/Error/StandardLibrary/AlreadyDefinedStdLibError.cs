using Antlr.Runtime.Tree;

namespace Tiger
{
    public abstract class AlreadyDefinedStdLibError : Error
    {
        public AlreadyDefinedStdLibError(ITree tree, string identifier) : base(tree, "{0} is already defined in Tiger's Standard Library and can't be redefined", identifier) { }
    }
}
