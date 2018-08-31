using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Specifies the functions declarations sequence
    /// </summary>
    public class FunctionDeclarationSequenceNode : DeclarationSequenceNode
    {
        private List<FunctionDeclarationNode> declarations;
        /// <summary>
        /// The list that contains all declared functions in the sequence
        /// </summary>
        public List<FunctionDeclarationNode> DeclaredFunctions
        {
            get
            {
                if (declarations == null)
                {
                    declarations = new List<FunctionDeclarationNode>();
                    for (int i = 0; i < ChildCount; i++)
                        declarations.Add(GetChild(i) as FunctionDeclarationNode);
                }
                return declarations;
            }
        }
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            foreach (var function in DeclaredFunctions)
            {
                //Analize return type of the function
                function.TypeDeclared = NilType.GetInstance;
                if (function.ReturnTypeIdentifier == null)
                    function.TypeDeclared = VoidType.GetInstance;
                else
                {
                    var function_return_type = scope.GetType(function.ReturnTypeIdentifier);
                    if (function_return_type == null)
                        errors.Add(new TypeNotFoundError(function, function.Identifier));
                    else
                        function.TypeDeclared = function_return_type;
                }

                //Analize return types for each parameter
                foreach (var parameter in function.Parameters)
                {
                    parameter.TypeDeclared = NilType.GetInstance;
                    var param_type = scope.GetType(parameter.TypeName);
                    if (param_type == null)
                        errors.Add(new TypeNotFoundError(parameter, parameter.Identifier));
                    else
                        parameter.TypeDeclared = param_type;
                }

                //Check for another function or variable that has the same identifier, else add function to scope
                var same_name_function = scope.GetFunction(function.Identifier);
                var same_name_variable = scope.GetVariable(function.Identifier);
                if (StandardLibrary.FunctionIds.Contains(function.Identifier))
                    errors.Add(new FunctionAlreadyDefinedStdLibError(function, function.Identifier));
                else if (same_name_function != null && same_name_function is FunctionDeclarationNode)
                    errors.Add(new FunctionAlreadyFoundError(function.GetChild(0), function.Identifier));
                else if (same_name_variable != null && same_name_variable is VariableDeclarationNode)
                    errors.Add(new VariableAlreadyFoundError(function.GetChild(0), function.Identifier));                
                else
                    scope.FunctionDictionary.Add(function.Identifier, function);
            }

            //Check semantics for each parameter
            foreach (var function in DeclaredFunctions)
                function.CheckSemantics(scope, errors);

            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (var func in DeclaredFunctions)
                func.MethodBuilder = type_builder.DefineMethod
                    (func.Identifier,
                    MethodAttributes.Public | MethodAttributes.Static,
                    func.TypeDeclared.BaseType,
                    func.Parameters.Select(param => param.TypeDeclared.BaseType).ToArray());

            foreach (var func in DeclaredFunctions)
                func.GenerateCode(code_generator, type_builder, module_builder);
        }
    }
}
