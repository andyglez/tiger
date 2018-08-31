using System;

namespace Tiger
{
    public abstract class TigerType
    {
        public abstract Type BaseType { get; }
        public abstract string Id { get; }

        public virtual bool Equals(TigerType t)
        {
            return t.Id == Id || t is NilType;
        }
        public override string ToString() { return Id; }
    }
}
