using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class SubstringFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = StringType.GetInstance.Id };
            function.Identifier = "substring";
            function.Parameters = new List<FunctionParameterNode>() {
                new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "s" },
                new FunctionParameterNode() { TypeDeclared = IntType.GetInstance, Identifier = "first" },
                new FunctionParameterNode() { TypeDeclared = IntType.GetInstance, Identifier = "n" } };
            function.TypeDeclared = StringType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("substring", MethodAttributes.Public | MethodAttributes.Static,
                                                               typeof(string), new Type[] { typeof(string), typeof(int), typeof(int) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Ldarg_1);
            il_generator.Emit(OpCodes.Ldarg_2);
            il_generator.Emit(OpCodes.Call, typeof(String).GetMethod("Substring", new Type[] { typeof(int), typeof(int) }));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }
    }
}
