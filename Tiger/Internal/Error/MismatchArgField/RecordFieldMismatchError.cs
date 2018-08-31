using Antlr.Runtime.Tree;

namespace Tiger
{
    public class RecordFieldMismatchError : ArgFieldError
    {
        public RecordFieldMismatchError(ITree tree, string field_name, string field_return_type, string creation_return_type)
            : base(tree, string.Format("Field {0} has type {1}, instead {2} was found and", field_name, field_return_type, creation_return_type)) { }
    }
}
