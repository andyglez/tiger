namespace Tiger
{
    /// <summary>
    /// Represents an abstraction of a declaration
    /// </summary>
    public abstract class DeclarationNode : Node
    {
        /// <summary>
        /// Gets or sets the type declared for the current declaration
        /// </summary>
        public TigerType TypeDeclared { get; set; }
    }
}
