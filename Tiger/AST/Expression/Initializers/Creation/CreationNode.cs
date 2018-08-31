namespace Tiger
{
    /// <summary>
    /// Represents an abstraction of a record, array or a field parameter creation
    /// </summary>
    public abstract class CreationNode : ExpressionNode
    {
        /// <summary>
        /// Gets the identifier of the variable created
        /// </summary>
        public string Identifier { get { return Children[0].Text; } }
    }
}
