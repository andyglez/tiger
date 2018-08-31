using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class GetLineFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = StringType.GetInstance.Id };
            function.Identifier = "getline";
            function.Parameters = new List<FunctionParameterNode>();
            function.TypeDeclared = StringType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("getline", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(string), new Type[] { });
            var il_generator = method_builder.GetILGenerator();

            il_generator.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine", new Type[] { }));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }
    }
}
