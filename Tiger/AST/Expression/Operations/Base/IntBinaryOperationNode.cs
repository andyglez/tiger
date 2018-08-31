using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a binary operation for int
    /// </summary>
    public class IntBinaryOperationNode : BinaryOperationNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            ReturnType = IntType.GetInstance;
            bool no_int_left = false;
            bool no_int_right = false;
            if (!LeftOperand.ReturnType.Equals(IntType.GetInstance))
            {
                errors.Add(new Error(LeftOperand, "Left operand doesn't return a int value"));
                no_int_left = true;
            }
            if (!RightOperand.ReturnType.Equals(IntType.GetInstance))
            {
                errors.Add(new Error(RightOperand, "Right operand doesn't return a int value"));
                no_int_right = true;
            }
            if (no_int_left && no_int_right)
                ReturnType = VoidType.GetInstance;
            return errors.Count > 0;
        }
        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
    }
}
