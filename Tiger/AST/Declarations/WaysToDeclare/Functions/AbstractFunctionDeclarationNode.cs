using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// This is an abstraction of a function declaration
    /// </summary>
    public abstract class AbstractFunctionDeclarationNode : DeclarationNode
    {
        public MethodBuilder MethodBuilder { get; set; }
        public ExpressionNode Body { get { return Children[2] as ExpressionNode; } }
        public abstract string Identifier { get; set; }
    }
}
