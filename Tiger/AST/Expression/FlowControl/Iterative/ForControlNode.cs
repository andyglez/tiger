using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents a definition of a for loop
    /// </summary>
    public class ForControlNode : IterativeNode
    {
        /// <summary>
        /// Gets the iterative variable declared for this loop
        /// </summary>
        public ForVarDeclarationNode IterativeVariable { get { return Children[0] as ForVarDeclarationNode; } }
        /// <summary>
        /// Gets the biggest value that the iterative variable will get
        /// </summary>
        public ExpressionNode UpperBoundExpression { get { return Children[1] as ExpressionNode; } } 

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            var child_scope = new Scope(scope);
            IterativeVariable.CheckSemantics(child_scope, errors);
            UpperBoundExpression.CheckSemantics(child_scope, errors);
            Body.CheckSemantics(child_scope, errors);

            ReturnType = VoidType.GetInstance;

            if (!UpperBoundExpression.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new UpperBoundError(UpperBoundExpression, UpperBoundExpression.ReturnType.Id));
            if (!Body.ReturnType.Equals(VoidType.GetInstance))
                errors.Add(new ForBodyReturnError(Body, Body.ReturnType.Id));
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Label forStart = code_generator.DefineLabel();
            Label forEnd = code_generator.DefineLabel();
            EndLabel = forEnd;

            IterativeVariable.GenerateCode(code_generator, type_builder, module_builder);

            UpperBoundExpression.GenerateCode(code_generator, type_builder, module_builder);

            code_generator.MarkLabel(forStart);

            code_generator.Emit(OpCodes.Dup);
            code_generator.Emit(OpCodes.Ldsfld, IterativeVariable.VariableBuilder);
            code_generator.Emit(OpCodes.Blt, forEnd);

            Body.GenerateCode(code_generator, type_builder, module_builder);

            code_generator.Emit(OpCodes.Ldsfld, IterativeVariable.VariableBuilder);
            code_generator.Emit(OpCodes.Ldc_I4_1);
            code_generator.Emit(OpCodes.Add);
            code_generator.Emit(OpCodes.Stsfld, IterativeVariable.VariableBuilder);
            code_generator.Emit(OpCodes.Br, forStart);

            code_generator.MarkLabel(forEnd);

            code_generator.Emit(OpCodes.Pop);
            IterativeVariable.RestoreVariable(code_generator);
        }
    }
}
