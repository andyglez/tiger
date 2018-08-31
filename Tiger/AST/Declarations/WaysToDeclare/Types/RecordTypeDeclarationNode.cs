using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides for a record type definition
    /// </summary>
    public class RecordTypeDeclarationNode : TypeDeclarationNode
    {
        /// <summary>
        /// Gets or sets the type builder for this record
        /// </summary>
        public TypeBuilder TypeBuilder { get; set; }

        private List<RecordFieldDeclarationNode> fields;
        /// <summary>
        /// Gets the list that contains all of the record fields
        /// </summary>
        public List<RecordFieldDeclarationNode> Fields
        {
            get
            {
                if (fields == null)
                {
                    fields = new List<RecordFieldDeclarationNode>();
                    for (int i = 0; i < Children[1].ChildCount; i++)
                        fields.Add(Children[1].GetChild(i) as RecordFieldDeclarationNode);
                }
                return fields;
            }
        }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Check that there is no redefining to the standard types
            if (Identifier.Equals(IntType.GetInstance.Id) || Identifier.Equals(StringType.GetInstance.Id))
                errors.Add(new TypeAlreadyDefinedStdLibError(GetChild(0), Identifier));

            foreach (var field in Fields)
            {
                field.CheckSemantics(scope, errors);
                if (!((RecordType)TypeDeclared).AddMember(field.Identifier, field))
                    errors.Add(new RecordFieldAlreadyFoundError(field.GetChild(0), field.Identifier, Identifier));
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (var field in Fields)
                field.VariableBuilder = 
                    TypeBuilder.DefineField(
                        field.Identifier,
                        field.TypeDeclared.BaseType, 
                        FieldAttributes.Public);
            TypeBuilder.CreateType();
        }
    }
}
