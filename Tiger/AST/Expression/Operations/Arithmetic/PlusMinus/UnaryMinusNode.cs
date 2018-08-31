using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the negation unary operation
    /// </summary>
    public class UnaryMinusNode : UnaryOperationNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            Expression.CheckSemantics(scope, errors);
            if (!Expression.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new Error(Expression, "Can't negate a non-integer expression"));
            ReturnType = IntType.GetInstance;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Expression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Neg);
        }
    }
}
