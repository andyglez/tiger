using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// This is an abstraction of a variable declaration
    /// </summary>
    public abstract class VariableDeclarationNode : DeclarationNode
    {
        private string id;
        /// <summary>
        /// Gets or sets the variable identifier (or name)
        /// </summary>
        public string Identifier { get { return id ?? (id = Children[0].Text); } set { id = value; } }
        /// <summary>
        /// Gets the definition of the right side of the declaration
        /// </summary>
        public ExpressionNode InitializeVar { get { return Children[1] as ExpressionNode; } }
        /// <summary>
        /// Gets or sets the field builder for creating the variable in runtime
        /// </summary>
        public FieldBuilder VariableBuilder { get; set; }
        /// <summary>
        /// Gets or sets the local builder for creating the variable in runtime
        /// </summary>
        public LocalBuilder LocalBuilder { get; set; }
        /// <summary>
        /// Retrieves the value in the code generation stage for evaluation in runtime
        /// </summary>
        /// <param name="codeGenerator">Generates Microsoft intermediate language (MSIL) instructions.</param>
        public abstract void GetValue(ILGenerator codeGenerator);
    }
}
