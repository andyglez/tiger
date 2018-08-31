using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents an array access
    /// </summary>
    public class ArrayAccessNode : LValueNode
    {
        /// <summary>
        /// Gets the accesing expression within the brackets
        /// </summary>
        public ExpressionNode AccesserExpression { get { return Children[0] as ExpressionNode; } }
        /// <summary>
        /// Gets the array construction expression
        /// </summary>
        public ExpressionNode Array { get { return Children[1] as ExpressionNode; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            Array.CheckSemantics(scope, errors);
            if (Array.ReturnType is ArrayType)
                ReturnType = ((ArrayType)Array.ReturnType).ElementsType;
            else
            {
                errors.Add(new MustBeArrayError(Array, Array.ReturnType.Id));
                ReturnType = BadType.GetInstance;
            }

            AccesserExpression.CheckSemantics(scope, errors);
            if (!AccesserExpression.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new IndexerError(AccesserExpression, AccesserExpression.ReturnType.Id));
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Array.GenerateCode(code_generator, type_builder, module_builder);
            AccesserExpression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Ldelem, ReturnType.BaseType);
        }

        public override void SetAssingCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder, ExpressionNode assingnation)
        {
            Array.GenerateCode(code_generator, type_builder, module_builder);
            AccesserExpression.GenerateCode(code_generator, type_builder, module_builder);
            assingnation.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Stelem, assingnation.ReturnType.BaseType);
        }
    }
}
