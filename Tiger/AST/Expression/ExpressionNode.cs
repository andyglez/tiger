namespace Tiger
{
    /// <summary>
    /// Contains an abstract definition for an expression node of the AST
    /// </summary>
    public abstract class ExpressionNode : Node
    {
        /// <summary>
        /// Gets or sets the return type of each expression
        /// </summary>
        public TigerType ReturnType { get; set; }
    }
}
