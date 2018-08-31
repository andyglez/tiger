using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the pair (identifier, value) of a creation field for a record
    /// </summary>
    public class FieldInitPairNode : Node
    {
        /// <summary>
        /// Gets the name of the field
        /// </summary>
        public string Name { get { return Children[0].Text; } }
        /// <summary>
        /// Gets the initializer expression of the field
        /// </summary>
        public ExpressionNode Expression { get { return Children[1] as ExpressionNode; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            return Expression.CheckSemantics(scope, errors);
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Expression.GenerateCode(code_generator, type_builder, module_builder);
        }
    }
}
