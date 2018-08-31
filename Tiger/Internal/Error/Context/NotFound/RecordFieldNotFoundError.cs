using Antlr.Runtime.Tree;

namespace Tiger
{
    public class RecordFieldNotFoundError : NotFoundError
    {
        public RecordFieldNotFoundError(ITree tree, string record_name, string field_name) : base(tree, string.Format("Bad index for field {0} of record {1}", field_name, record_name)) { }
        public RecordFieldNotFoundError(ITree tree, string record_name, int index) 
            : base(tree, string.Format("Field at index {0} of record {1}", index, record_name)) { }

    }
}
