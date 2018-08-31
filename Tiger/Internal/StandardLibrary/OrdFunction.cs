using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class OrdFunction : StdLibFunction
    {
        public override FunctionDeclarationNode GetFunctionNode()
        {
            function = new FunctionDeclarationNode() { ReturnTypeIdentifier = IntType.GetInstance.Id };
            function.Identifier = "ord";
            function.Parameters = new List<FunctionParameterNode>() { new FunctionParameterNode() { TypeDeclared = StringType.GetInstance, Identifier = "str" } };
            function.TypeDeclared = IntType.GetInstance;
            return function;
        }
        public override void GenerateFunction(TypeBuilder type_builder)
        {
            var method_builder = type_builder.DefineMethod("ord", MethodAttributes.Public | MethodAttributes.Static,
                                                         typeof(int), new Type[] { typeof(string) });
            var il_generator = method_builder.GetILGenerator();
            Label empty = il_generator.DefineLabel();

            il_generator.Emit(OpCodes.Ldarg_0);
            il_generator.Emit(OpCodes.Dup);
            il_generator.Emit(OpCodes.Call, typeof(string).GetMethod("get_Length", Type.EmptyTypes));
            il_generator.Emit(OpCodes.Brfalse, empty);

            il_generator.Emit(OpCodes.Ldc_I4_0);
            il_generator.Emit(OpCodes.Call, typeof(string).GetMethod("get_Chars", new Type[] { typeof(int) }));
            il_generator.Emit(OpCodes.Ret);

            il_generator.MarkLabel(empty);

            il_generator.Emit(OpCodes.Pop);
            il_generator.Emit(OpCodes.Ldc_I4, -1);
            il_generator.Emit(OpCodes.Ret);
            function.MethodBuilder = method_builder;
        }

    }
}
