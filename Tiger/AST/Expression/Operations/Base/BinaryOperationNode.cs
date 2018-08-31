namespace Tiger
{
    /// <summary>
    /// Represents a binary operation
    /// </summary>
    public abstract class BinaryOperationNode : ExpressionNode
    {
        /// <summary>
        /// Gets the left operand expression
        /// </summary>
        public ExpressionNode LeftOperand { get { return Children[0] as ExpressionNode; } }
        /// <summary>
        /// Gets the right operand expression
        /// </summary>
        public ExpressionNode RightOperand { get { return Children[1] as ExpressionNode; } }
    }
}
