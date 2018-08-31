using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Tiger
{
    public class RecordType : TigerType
    {
        public override string Id { get { return type_name; } }
        public override Type BaseType { get { return TypeBuilder; } }

        public TypeBuilder TypeBuilder { get; set; }
        public int FieldCount { get { return fields.Count; } }

        private List<KeyValuePair<string, RecordFieldDeclarationNode>> fields;
        private string type_name;

        public RecordType(string type_name)
        {
            this.type_name = type_name;
            fields = new List<KeyValuePair<string, RecordFieldDeclarationNode>>();
        }
        public RecordType(string type_name, ModuleBuilder module_builder) : this(type_name)
        {
            TypeBuilder = module_builder.DefineType(type_name);
        }
        public RecordFieldDeclarationNode GetField(string name)
        {
            return (from pair in fields
                    where pair.Key.CompareTo(name) == 0
                    select pair.Value).FirstOrDefault();
        }


        public RecordFieldDeclarationNode GetField(int i, out string name)
        {
            if (i < fields.Count)
            {
                name = fields[i].Key;
                return fields[i].Value;
            }
            name = null;
            return null;
        }
        
        public bool AddMember(string name, RecordFieldDeclarationNode type)
        {
            bool result = true;
            foreach (var pair in fields)
                if (pair.Key.CompareTo(name) == 0)
                    result = false;

            fields.Add(new KeyValuePair<string, RecordFieldDeclarationNode>(name, type));
            return result;
        }
    }
}
