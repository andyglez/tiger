using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents an if-then statement
    /// </summary>
    public class IfThenNode : IfThenAbstractNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Check condition
            Condition.CheckSemantics(scope, errors);
            if (!Condition.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new IfConditionError(Condition, Condition.ReturnType.Id));
            //Check then statement
            ThenExpression.CheckSemantics(scope, errors);
            ReturnType = ThenExpression.ReturnType;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label end = code_generator.DefineLabel();
            Condition.GenerateCode(code_generator, type_builder, module_builder);

            code_generator.Emit(OpCodes.Brfalse, end);
            ThenExpression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.MarkLabel(end);
        }
    }
}
