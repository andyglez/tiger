using System;

namespace Tiger
{
    public class VoidType : TigerType
    {
        private static VoidType singleton;
        public static VoidType GetInstance { get { return singleton ?? (singleton = new VoidType()); } }

        public override Type BaseType { get { return typeof(void); } }
        public override string Id { get { return "void"; } }
        private VoidType() { }
        public override bool Equals(TigerType t) { return t is IntType ? false : true; }
    }
}
