using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides a definition for an assignment
    /// </summary>
    public class AssignmentNode : ExpressionNode
    {
        /// <summary>
        /// Gets the left value of the assignment statement
        /// </summary>
        public LValueNode Left { get { return Children[0] as LValueNode; } }
        /// <summary>
        /// Gets the right side of the assingment statement
        /// </summary>
        public ExpressionNode Right { get { return Children[1] as ExpressionNode; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            Left.CheckSemantics(scope, errors);
            Right.CheckSemantics(scope, errors);

            ReturnType = VoidType.GetInstance;
            if(Left is VariableNode)
            {
                var read_only = ((VariableNode)Left).VarDeclaration as ForVarDeclarationNode;
                if (read_only != null)
                    errors.Add(new ForReadOnlyError(Left, read_only.Identifier));
            }
            if (Right.ReturnType.Equals(VoidType.GetInstance))
                errors.Add(new AssignmentRigthSideError(Right));
            else if (!Left.ReturnType.Equals(Right.ReturnType))
                errors.Add(new AssignmentMismatchError(this, Left.ReturnType.Id, Right.ReturnType.Id));
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Left.SetAssingCode(code_generator, type_builder, module_builder, Right);
        }
    }
}
