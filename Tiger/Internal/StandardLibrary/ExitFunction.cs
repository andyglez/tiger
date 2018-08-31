using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class ExitFunction : StdLibFunction
    {
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("exit", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(void), new Type[] { typeof(int) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Call, typeof(Environment).GetMethod("Exit", new Type[] { typeof(int) }));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }

        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = VoidType.GetInstance.Id };
            function.Identifier = "exit";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = IntType.GetInstance, Identifier = "exitcode" } };
            function.TypeDeclared = VoidType.GetInstance;
            return function;
        }
    }
}
