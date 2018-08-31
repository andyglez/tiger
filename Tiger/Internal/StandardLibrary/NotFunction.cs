using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class NotFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = IntType.GetInstance.Id };
            function.Identifier = "not";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = IntType.GetInstance, Identifier = "i" } };
            function.TypeDeclared = IntType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("not", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(int), new Type[] { typeof(int) });
            var il_generator = method_builder.GetILGenerator();
            il_generator.Emit(OpCodes.Ldc_I4_0);
            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Ceq);
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }
    }
}
