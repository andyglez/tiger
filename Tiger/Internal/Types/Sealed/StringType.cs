using System;

namespace Tiger
{
    public sealed class StringType : TigerType
    {
        private static StringType singleton;
        public static StringType GetInstance { get { return singleton ?? (singleton = new StringType()); } }

        public override Type BaseType { get { return typeof(string); } }
        public override string Id { get { return "string"; } }

        private StringType() { }
        public override bool Equals(TigerType t) { return Id == t.Id; }
    }
}
