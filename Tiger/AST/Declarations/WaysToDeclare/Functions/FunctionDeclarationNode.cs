using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides definitions that determines how a function is declared
    /// </summary>
    public class FunctionDeclarationNode : AbstractFunctionDeclarationNode
    {
        private string id;
        /// <summary>
        /// Gets or sets the function identifier (or name)
        /// </summary>
        public override string Identifier { get { return id ?? (id = Children[0].Text); } set { id = value; } }
        private List<FunctionParameterNode> parameters;
        /// <summary>
        /// Contains the parameters defined for a function declaration
        /// </summary>
        public List<FunctionParameterNode> Parameters
        {
            get
            {
                if (parameters == null)
                {
                    parameters = new List<FunctionParameterNode>();
                    for (int i = 0; i < Children[1].ChildCount; i++)
                        parameters.Add(Children[1].GetChild(i) as FunctionParameterNode);
                }
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }
        private string return_type;
        /// <summary>
        /// Gets or sets the function return type name
        /// </summary>
        public string ReturnTypeIdentifier { get { return return_type ?? (return_type = ChildCount == 4 ? Children[3].Text : null); } set { return_type = value; } }


        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            var child_scope = new Scope(scope);
            //Check semantics in parameters
            int index = 0;
            foreach (var parameter in Parameters)
            {
                parameter.Index = index++;
                parameter.CheckSemantics(child_scope, errors);
            }
            //Check body expression
            var body_error = Body.CheckSemantics(child_scope, errors);

            //Match specified return type with the type that the body expression actually returns
            if (!TypeDeclared.Equals(Body.ReturnType))
                errors.Add(new FunctionMismatchReturnError(Body, Identifier, ReturnTypeIdentifier, Body.ReturnType.Id));
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            code_generator = MethodBuilder.GetILGenerator();
            foreach (var arg in Parameters)
                arg.GenerateCode(code_generator, type_builder, module_builder);
            Body.GenerateCode(code_generator, type_builder, module_builder);
            foreach (var arg in Parameters)
                arg.RestoreVariable(code_generator);
            code_generator.Emit(OpCodes.Ret);
        }
    }
}
