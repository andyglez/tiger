using System;

namespace Tiger
{
    public sealed class BadType : TigerType
    {
        public override Type BaseType { get { return typeof(object); } }

        public override string Id { get { return "bad"; } }

        private static BadType singleton;
        public static BadType GetInstance { get { return singleton ?? (singleton = new BadType()); } }

        public override bool Equals(TigerType t) { return false; }
        private BadType() { }
    }
}
