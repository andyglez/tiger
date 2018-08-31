namespace Tiger
{
    /// <summary>
    /// Represents an unary operation
    /// </summary>
    public abstract class UnaryOperationNode : ExpressionNode
    {
        /// <summary>
        /// Gets the expression to operate
        /// </summary>
        public ExpressionNode Expression { get { return Children[0] as ExpressionNode; } }
    }
}
