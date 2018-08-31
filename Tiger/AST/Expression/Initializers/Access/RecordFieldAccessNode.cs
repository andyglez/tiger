using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a record field access
    /// </summary>
    public class RecordFieldAccessNode : LValueNode
    {
        /// <summary>
        /// Gets the field identifier (or name)
        /// </summary>
        public string FieldIdentifier { get { return Children[0].Text; } }
        /// <summary>
        /// Gets the field initializer expression
        /// </summary>
        public ExpressionNode Record { get { return Children[1] as ExpressionNode; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Check semantics and by default there is a bad type related to this
            Record.CheckSemantics(scope, errors);
            ReturnType = BadType.GetInstance;
            if (!(Record.ReturnType is RecordType))
                errors.Add(new MustBeRecordError(Record, Record.ReturnType.Id));
            else
            {
                //Find out if there is a field with the specified name
                var field = ((RecordType)Record.ReturnType).GetField(FieldIdentifier);
                if (field == null)
                    errors.Add(new RecordFieldNotFoundError(GetChild(0), Record.ReturnType.Id, FieldIdentifier));
                else
                    ReturnType = field.TypeDeclared;
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            RecordType recType = (RecordType)Record.ReturnType;

            Record.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Ldfld, recType.GetField(FieldIdentifier).VariableBuilder);
        }

        public override void SetAssingCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder, ExpressionNode assingnation)
        {
            RecordType recType = (RecordType)Record.ReturnType;

            Record.GenerateCode(code_generator, type_builder, module_builder);
            assingnation.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Stfld, recType.GetField(FieldIdentifier).VariableBuilder);
        }
    }
}
