using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the (|) binary operation
    /// </summary>
    public class OrNode : IntBinaryOperationNode
    {
        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label trueLabel = code_generator.DefineLabel();
            Label endLabel = code_generator.DefineLabel();
            LeftOperand.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Brtrue, trueLabel);
            RightOperand.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Brtrue, trueLabel);
            code_generator.Emit(OpCodes.Ldc_I4_0);
            code_generator.Emit(OpCodes.Br, endLabel);
            code_generator.MarkLabel(trueLabel);
            code_generator.Emit(OpCodes.Ldc_I4_1);
            code_generator.MarkLabel(endLabel);
        }
    }
}
