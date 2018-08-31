using Antlr.Runtime.Tree;

namespace Tiger
{
    public class MustBeRecordError : Error
    {
        public MustBeRecordError(ITree tree, string type_name) 
            : base(tree, "Operator '.' can only be applied to record types, instead it was found a expression of type {0}", type_name) { }
        public MustBeRecordError(ITree tree, string type_name, string creation)
            : base(tree, "Creation of {0} must be defined as {0}, instead it was found {1}", creation, type_name) { }
    }
}
