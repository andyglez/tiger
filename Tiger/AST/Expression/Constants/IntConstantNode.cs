using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents an integer
    /// </summary>
    public class IntConstantNode : ExpressionNode
    {
        /// <summary>
        /// Gets the CSharp integer value of an int
        /// </summary>
        public int Value { get; private set; }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            ReturnType = IntType.GetInstance;
            int value;
            if(!int.TryParse(Text, out value))
                errors.Add(new IntError(this, Text));
            Value = value;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator.Emit(OpCodes.Ldc_I4, Value);
        }
    }
}
