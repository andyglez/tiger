using Antlr.Runtime.Tree;

namespace Tiger
{
    public class CircularDefinitionError : Error
    {
        public CircularDefinitionError(ITree tree, string id) : base(tree, "Type {0} is defined over itself", id) { }
    }
}
