using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides an abstraction for iteratives definitions like while or for
    /// </summary>
    public abstract class IterativeNode : ExpressionNode
    {
        /// <summary>
        /// Represents the body of execution for while or for loops
        /// </summary>
        public ExpressionNode Body { get { return Children[ChildCount - 1] as ExpressionNode; } }
        /// <summary>
        /// Represents a label in IL
        /// </summary>
        public Label EndLabel { get; set; }
    }
}
