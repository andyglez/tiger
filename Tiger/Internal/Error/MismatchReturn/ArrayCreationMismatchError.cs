using Antlr.Runtime.Tree;

namespace Tiger
{
    public class ArrayCreationMismatchError : Error
    {
        public ArrayCreationMismatchError(ITree tree, string initializer_identifier, string definition_identifier) 
            : base(tree, "Initializer expression returns {0} and the array is contained of {1}", initializer_identifier, definition_identifier) { }
    }
}
