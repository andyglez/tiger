using Antlr.Runtime.Tree;

namespace Tiger
{
    public class FieldCountError : ArgFieldError
    {
        public FieldCountError(ITree tree, string record_name, int field_count)
            : base(tree, string.Format("Creating record {0} must have {1} fields and", record_name, field_count)) { }
    }
}
