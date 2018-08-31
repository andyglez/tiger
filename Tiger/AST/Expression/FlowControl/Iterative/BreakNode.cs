using Antlr.Runtime.Tree;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a break statement
    /// </summary>
    public class BreakNode : IterativeNode
    {
        /// <summary>
        /// Gets or sets the iterative node that contains break
        /// </summary>
        public IterativeNode IterativeNode { get; set; }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //By default it returns no value
            ReturnType = VoidType.GetInstance;
            ITree actual = this;
            while (true)
            {
                //If no iterative node was found or another sort of declaration was found then is error
                if (actual.Parent == null || actual.Parent is FunctionDeclarationNode)
                {
                    errors.Add(new BreakError(this));
                    return errors.Count > 0;
                }
                //If it is contained within an iterative node
                if (actual.Parent is IterativeNode && actual == ((IterativeNode)actual.Parent).Body)
                {
                    IterativeNode = ((IterativeNode)actual.Parent);
                    return false;
                }
                //Search for within my parents parent
                if (actual.Parent is ExpressionListNode)
                    ((ExpressionListNode)actual.Parent).ReturnType = VoidType.GetInstance;
                actual = actual.Parent;
            }
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator.Emit(OpCodes.Br, IterativeNode.EndLabel);
        }
    }
}
