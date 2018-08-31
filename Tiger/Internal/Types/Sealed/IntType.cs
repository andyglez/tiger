using System;

namespace Tiger
{
    public sealed class IntType : TigerType
    {
        private static IntType singleton;
        public static IntType GetInstance { get { return singleton ?? (singleton = new IntType()); } }

        public override Type BaseType { get { return typeof(int); } }
        public override string Id { get { return "int"; } }

        private IntType() { }
        public override bool Equals(TigerType t) { return Id == t.Id; }
    }
}
