using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace Tiger
{
    public class Error : IComparable<Error>
    {
        public string Message { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }

        public Error(RecognitionException ex, string message)
        {
            Message = message;
            Line = ex.Line;
            Column = ex.CharPositionInLine;
        }
        public Error(ITree tree, string message, params object[] objs)
        {
            Message = string.Format(message, objs);
            Line = tree.Line;
            Column = tree.CharPositionInLine;
        }
        public int CompareTo(Error other)
        {
            if (other == null)
                return 1;
            var aux = Line.CompareTo(other.Line);
            return aux != 0 ? aux : Column.CompareTo(other.Column);
        }
    }
}
