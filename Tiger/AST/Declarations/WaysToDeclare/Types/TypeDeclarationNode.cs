namespace Tiger
{
    /// <summary>
    /// This is an abstraction of a type declaration
    /// </summary>
    public abstract class TypeDeclarationNode : DeclarationNode
    {
        /// <summary>
        /// Returns the type identifier (or name)
        /// </summary>
        public string Identifier { get { return Children[0].Text; } }
    }
}
