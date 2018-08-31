using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class ConcatFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = StringType.GetInstance.Id };
            function.Identifier = "concat";
            function.Parameters = new List<FunctionParameterNode>() {
                new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "s1" },
                new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "s2" } };
            function.TypeDeclared = StringType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("concat", MethodAttributes.Public | MethodAttributes.Static,
                                                        typeof(string), new Type[] { typeof(string), typeof(string) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Ldarg_1);
            il_generator.Emit(OpCodes.Call, typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string) }));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }
    }
}
