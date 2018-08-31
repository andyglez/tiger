using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the nil value
    /// </summary>
    public class NilConstantNode : ExpressionNode
    {
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            ReturnType = NilType.GetInstance;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator.Emit(OpCodes.Ldnull);
        }
    }
}
