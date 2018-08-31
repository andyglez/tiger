using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the (&) binary operation
    /// </summary>
    public class AndNode : IntBinaryOperationNode
    {
        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label falseLabel = code_generator.DefineLabel();
            Label endLabel = code_generator.DefineLabel();
            LeftOperand.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Brfalse, falseLabel);
            RightOperand.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Brfalse, falseLabel);
            code_generator.Emit(OpCodes.Ldc_I4_1);
            code_generator.Emit(OpCodes.Br, endLabel);
            code_generator.MarkLabel(falseLabel);
            code_generator.Emit(OpCodes.Ldc_I4_0);
            code_generator.MarkLabel(endLabel);
        }
    }
}
