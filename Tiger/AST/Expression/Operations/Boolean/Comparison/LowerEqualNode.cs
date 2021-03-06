﻿using System;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the (lower) binary operation
    /// </summary>
    public class LowerEqualNode : SameValuesOperationNode
    {
        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            //Left Code
            LeftOperand.GenerateCode(code_generator, type_builder, module_builder);
            //Right Code
            RightOperand.GenerateCode(code_generator, type_builder, module_builder);

            Label endLabel = code_generator.DefineLabel();
            Label trueLabel = code_generator.DefineLabel();

            if (LeftOperand.ReturnType.Equals(StringType.GetInstance))
            {
                code_generator.Emit(OpCodes.Call, typeof(String).GetMethod("CompareTo", new Type[] { typeof(string) }));
                code_generator.Emit(OpCodes.Ldc_I4_0);
            }
            code_generator.Emit(OpCodes.Ble, trueLabel);

            code_generator.Emit(OpCodes.Ldc_I4_0);
            code_generator.Emit(OpCodes.Br, endLabel);

            code_generator.MarkLabel(trueLabel);
            code_generator.Emit(OpCodes.Ldc_I4_1);

            code_generator.MarkLabel(endLabel);
        }
    }
}
