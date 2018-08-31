using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class SizeFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = IntType.GetInstance.Id };
            function.Identifier = "size";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "strg" } };
            function.TypeDeclared = IntType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("size", MethodAttributes.Public | MethodAttributes.Static,
                                                          typeof(int), new Type[] { typeof(string) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Call, typeof(string).GetMethod("get_Length", Type.EmptyTypes));
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }

        
    }
}
