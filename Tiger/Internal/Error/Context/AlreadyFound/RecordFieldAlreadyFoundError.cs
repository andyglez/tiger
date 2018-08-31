using Antlr.Runtime.Tree;

namespace Tiger
{
    public class RecordFieldAlreadyFoundError : AlreadyFoundError
    {
        public RecordFieldAlreadyFoundError(ITree tree, string field_identifier, string record_identifier) 
            : base(tree, string.Format("Field '{0}' of record '{1}'", field_identifier, record_identifier)) { }

    }
}
