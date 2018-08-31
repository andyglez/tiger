using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a string
    /// </summary>
    public class StringConstantNode : ExpressionNode
    {
        /// <summary>
        /// Gets the CSharp value of a string
        /// </summary>
        public string Value { get; private set; }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            ReturnType = StringType.GetInstance;
            Value = StringConstant.EscapeText(Text);
            if(Value == null)
                errors.Add(new StringError(this));
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator.Emit(OpCodes.Ldstr, Value);
        }
    }
}
