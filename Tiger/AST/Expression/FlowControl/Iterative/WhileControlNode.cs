using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a definition for a while statement
    /// </summary>
    public class WhileControlNode : IterativeNode
    {
        /// <summary>
        /// Gets the condition expression
        /// </summary>
        public ExpressionNode Condition { get { return Children[0] as ExpressionNode; } }
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            Condition.CheckSemantics(scope, errors);
            if (!Condition.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new WhileConditionError(Condition, Condition.ReturnType.Id));
            Body.CheckSemantics(scope, errors);
            if (!Body.ReturnType.Equals(VoidType.GetInstance))
                errors.Add(new WhileBodyReturnError(Body, Body.ReturnType.Id));
            ReturnType = VoidType.GetInstance;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label whileStart = code_generator.DefineLabel();
            Label whileEnd = code_generator.DefineLabel();
            EndLabel = whileEnd;

            code_generator.MarkLabel(whileStart);

            Condition.GenerateCode(code_generator, type_builder, module_builder);

            code_generator.Emit(OpCodes.Ldc_I4_0);
            code_generator.Emit(OpCodes.Beq, whileEnd);

            Body.GenerateCode(code_generator, type_builder, module_builder);

            code_generator.Emit(OpCodes.Br, whileStart);
            code_generator.MarkLabel(whileEnd);
        }
    }
}
