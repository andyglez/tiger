using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class ChrFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = StringType.GetInstance.Id };
            function.Identifier = "chr";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = IntType.GetInstance, Identifier = "number" } };
            function.TypeDeclared = StringType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("chr", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(string), new Type[] { typeof(int) });
            var il_generator = method_builder.GetILGenerator();
            Label error = il_generator.DefineLabel();

            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Dup);
            il_generator.Emit(OpCodes.Ldc_I4_0);
            il_generator.Emit(OpCodes.Blt, error);

            il_generator.Emit(OpCodes.Dup);
            il_generator.Emit(OpCodes.Ldc_I4, 256);
            il_generator.Emit(OpCodes.Bge, error);

            il_generator.Emit(OpCodes.Conv_U2);
            il_generator.Emit(OpCodes.Call, typeof(Char).GetMethod("ToString", new Type[] { typeof(char) }));
            il_generator.Emit(OpCodes.Ret);

            il_generator.MarkLabel(error);
            il_generator.ThrowException(typeof(ArgumentException));
            function.MethodBuilder = method_builder;
        }
    }
}
