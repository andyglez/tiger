using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a list of expressions
    /// </summary>
    public class ExpressionListNode : ExpressionNode
    {
        private List<ExpressionNode> seq;
        /// <summary>
        /// Gets the list of expressions
        /// </summary>
        public List<ExpressionNode> Sequence
        {
            get
            {
                if (seq == null)
                {
                    seq = new List<ExpressionNode>();
                    for (int i = 0; i < ChildCount; i++)
                        seq.Add(Children[i] as ExpressionNode);
                }
                return seq;
            }
        }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            foreach (var expression in Sequence)
                expression.CheckSemantics(scope, errors);
            if (ReturnType == VoidType.GetInstance)
                return errors.Count > 0;
            ReturnType = Sequence.Count == 0 ? VoidType.GetInstance : Sequence[Sequence.Count - 1].ReturnType;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            for (int i = 0; i < Sequence.Count; i++)
            {
                Sequence[i].GenerateCode(code_generator, type_builder, module_builder);
                if (!Sequence[i].ReturnType.Equals(VoidType.GetInstance) && i < Sequence.Count - 1)
                    code_generator.Emit(OpCodes.Pop);
            }
        }
    }
}
