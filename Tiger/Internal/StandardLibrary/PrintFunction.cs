using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class PrintFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = VoidType.GetInstance.Id };
            function.Identifier = "print";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "text" } };
            function.TypeDeclared = VoidType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("print", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(void), new Type[] { typeof(string) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }
    }
}
