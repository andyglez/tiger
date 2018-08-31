using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a binary operation that can't have distinct types of values
    /// </summary>
    public class DistinctValuesOperationNode : BinaryOperationNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Check semantics
            LeftOperand.CheckSemantics(scope, errors);
            RightOperand.CheckSemantics(scope, errors);

            ReturnType = VoidType.GetInstance;
            //int-int or string-string supported only
            if ((LeftOperand.ReturnType.Equals(IntType.GetInstance) && RightOperand.ReturnType.Equals(StringType.GetInstance)) ||
                (LeftOperand.ReturnType.Equals(StringType.GetInstance) && RightOperand.ReturnType.Equals(IntType.GetInstance)))
                errors.Add(new Error(this, "Binary operators can't be applied to string and int types"));

            if (LeftOperand.ReturnType.Equals(IntType.GetInstance) && RightOperand.ReturnType.Equals(IntType.GetInstance))
                ReturnType = IntType.GetInstance;
            else if (LeftOperand.ReturnType.Equals(StringType.GetInstance) && RightOperand.ReturnType.Equals(StringType.GetInstance))
                ReturnType = StringType.GetInstance;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
    }
}
