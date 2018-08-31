using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides definition for a function call
    /// </summary>
    public class FunctionCallNode : ExpressionNode
    {
        /// <summary>
        /// Gets the function identifier (or name)
        /// </summary>
        public string FunctionName { get { return Children[0].Text; } }
        private List<ExpressionNode> args;
        /// <summary>
        /// Gets a list that contains the arguments for the function call
        /// </summary>
        public List<ExpressionNode> Arguments
        {
            get
            {
                if (args == null)
                {
                    args = new List<ExpressionNode>();
                    for (int i = 0; i < Children[1].ChildCount; i++)
                        args.Add(Children[1].GetChild(i) as ExpressionNode);
                }
                return args;
            }
        }
        
        private FunctionDeclarationNode function;


        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //If is a stdlib function mark for future process
            if (StandardLibrary.FunctionIds.Contains(FunctionName))
                StandardLibrary.UsedFunctions.Add(FunctionName);
            //Check semantics for each argument
            foreach (var arg in Arguments)
                arg.CheckSemantics(scope, errors);

            //Check for existances
            function = scope.GetFunction(FunctionName);
            if(function == null)
            {
                errors.Add(new FunctionNotFoundError(GetChild(0), FunctionName));
                ReturnType = NilType.GetInstance;
            }
            else
            {
                //The parameters must match with the declarations
                if(function.Parameters.Count == Arguments.Count)
                {
                    for (int i = 0; i < Arguments.Count; i++)
                        if (!function.Parameters[i].TypeDeclared.Equals(Arguments[i].ReturnType))
                            errors.Add(new FunctionParameterError(Arguments[i], FunctionName, i + 1, function.Parameters[i].TypeDeclared.Id));
                }
                else
                    errors.Add(new ArgCountError(GetChild(0), FunctionName, function.Parameters.Count));
                ReturnType = function.TypeDeclared;
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (ExpressionNode arg in Arguments)
                arg.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Call, function.MethodBuilder);
        }
        
    }
}
