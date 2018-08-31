using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents an if-then-else statement
    /// </summary>
    public class IfThenElseNode : IfThenAbstractNode
    {
        /// <summary>
        /// Gets an else expression statement
        /// </summary>
        public ExpressionNode ElseExpression { get { return Children[2] as ExpressionNode; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            Condition.CheckSemantics(scope, errors);
            if (!Condition.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new IfConditionError(Condition, Condition.ReturnType.Id));
            ThenExpression.CheckSemantics(scope, errors);
            ElseExpression.CheckSemantics(scope, errors);
            if (!ThenExpression.ReturnType.Equals(ElseExpression.ReturnType))
            {
                errors.Add(new IfThenElseMismatchError(this, ThenExpression.ReturnType.Id, ElseExpression.ReturnType.Id));
                ReturnType = BadType.GetInstance;
            }
            else
                ReturnType = ThenExpression.ReturnType is BadType ? ElseExpression.ReturnType : ThenExpression.ReturnType;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label thenLabel = code_generator.DefineLabel();
            Label endLabel = code_generator.DefineLabel();

            Condition.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Brfalse, thenLabel);
            ThenExpression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Br, endLabel);
            code_generator.MarkLabel(thenLabel);
            ElseExpression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.MarkLabel(endLabel);
        }
    }
}
