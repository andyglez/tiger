namespace Tiger
{
    /// <summary>
    /// Represents an abstraction for if-then statement
    /// </summary>
    public abstract class IfThenAbstractNode : ExpressionNode
    {
        /// <summary>
        /// Gets the condition expression of an if statement
        /// </summary>
        public ExpressionNode Condition { get { return Children[0] as ExpressionNode; } }
        /// <summary>
        /// Gets the then body expression of the then statement
        /// </summary>
        public ExpressionNode ThenExpression { get { return Children[1] as ExpressionNode; } }
    }
}
