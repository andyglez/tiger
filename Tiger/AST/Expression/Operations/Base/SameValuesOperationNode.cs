using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a binary operation that must have the same type
    /// </summary>
    public abstract class SameValuesOperationNode : BinaryOperationNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);
            if (!LeftOperand.ReturnType.Equals(RightOperand.ReturnType))
                errors.Add(
                    new Error(this,
                    "Operator '{0}' supports only the same types for each side, instead it was found '{1}' and '{2}'",
                    Text,
                    LeftOperand.ReturnType.Id,
                    RightOperand.ReturnType.Id));
            ReturnType = IntType.GetInstance;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
    }
}
