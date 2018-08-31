using System;

namespace Tiger
{
    public sealed class NilType : TigerType
    {
        private static NilType singleton;
        public static NilType GetInstance { get { return singleton ?? (singleton = new NilType()); } }

        public override Type BaseType { get { return typeof(object); } }
        public override string Id { get { return "nil"; } }

        private NilType() { }
        public override bool Equals(TigerType t) { return !(t is VoidType || t is IntType); }
    }
}
