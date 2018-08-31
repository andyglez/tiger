using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the way to create an instance of a record
    /// </summary>
    public class RecordCreationNode : CreationNode
    {
        private List<FieldInitPairNode> fields;
        /// <summary>
        /// Gets the fields for this record
        /// </summary>
        public List<FieldInitPairNode> Fields
        {
            get
            {
                if(fields == null)
                {
                    fields = new List<FieldInitPairNode>();
                    for (int i = 0; i < Children[1].ChildCount; i++)
                        fields.Add(Children[1].GetChild(i) as FieldInitPairNode);
                }
                return fields;
            }
        }
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            var type = scope.GetType(Identifier);
            ReturnType = BadType.GetInstance;
            //Not found
            if(type == null)
            {
                errors.Add(new TypeNotFoundError(GetChild(0), Identifier));
                return true;
            }
            //Not a record
            var record = type as RecordType;
            if(record == null)
            {
                errors.Add(new MustBeRecordError(GetChild(0), Identifier, "record"));
                return true;
            }

            ReturnType = type;
            //Check each field
            if (Fields.Count != record.FieldCount)
                errors.Add(new FieldCountError(this, record.Id, record.FieldCount));
            for (int i = 0; i < Fields.Count; i++)
            {
                var field = Fields[i];
                field.CheckSemantics(scope, errors);
                string definition_name;
                var field_declaration = record.GetField(i, out definition_name);
                if (definition_name == null)
                    errors.Add(new RecordFieldNotFoundError(field.GetChild(0), Identifier, i + 1));
                else if (definition_name != field.Name)
                    errors.Add(new RecordFieldNotFoundError(field.GetChild(0), Identifier, field.Name));
                else if (!field.Expression.ReturnType.Equals(field_declaration.TypeDeclared))
                    errors.Add(new RecordFieldMismatchError(field.Expression, field.Name, field_declaration.TypeDeclared.Id, field.Expression.ReturnType.Id));
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator.Emit(OpCodes.Newobj, ((RecordType)ReturnType).TypeBuilder.GetConstructor(new Type[] { }));

            foreach (var memberInit in Fields)
            {
                code_generator.Emit(OpCodes.Dup);
                memberInit.GenerateCode(code_generator, type_builder, module_builder);
                code_generator.Emit(OpCodes.Stfld, ((RecordType)ReturnType).GetField(memberInit.Name).VariableBuilder);
            }
        }
    }
}
