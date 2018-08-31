using Antlr.Runtime.Tree;

namespace Tiger
{
    public class IndexerError : MustBeIntError
    {
        public IndexerError(ITree tree, string return_type) : base(tree, "Array indexer's return type", return_type) { }

    }
    public class ArraySizeError : MustBeIntError
    {
        public ArraySizeError(ITree tree, string return_type) : base(tree, "Array size expression return type", return_type) { }

    }
}
