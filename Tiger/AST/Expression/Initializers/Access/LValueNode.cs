using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents an abstraction of left value of a statement
    /// </summary>
    public abstract class LValueNode : ExpressionNode
    {
        /// <summary>
        /// Sets the IL parallel of a left side of a statement
        /// </summary>
        /// <param name="codeGenerator"></param>
        /// <param name="typeBuilder"></param>
        /// <param name="moduleBuilder"></param>
        /// <param name="assingExp"></param>
        public abstract void SetAssingCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder, ExpressionNode assingnation);
    }
}
