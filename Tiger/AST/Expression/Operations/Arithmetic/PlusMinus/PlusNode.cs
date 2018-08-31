﻿using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the (+) binary operation
    /// </summary>
    public class PlusNode : DistinctValuesOperationNode
    {
        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            LeftOperand.GenerateCode(code_generator, type_builder, module_builder);
            RightOperand.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Add);
        }
    }
}
