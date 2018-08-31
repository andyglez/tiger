using System;

namespace Tiger
{
    public sealed class ArrayType : TigerType
    {
        public override Type BaseType { get { return Array.CreateInstance(ElementsType.BaseType, 1).GetType(); } }
        public override string Id { get { return string.Format("array of {0}", ElementsType.Id); } }
        public TigerType ElementsType { get; set; }
        public string Name { get; set; }

        public ArrayType(TigerType elements, string name)
        {
            ElementsType = elements;
            Name = name;
        }
    }
}
