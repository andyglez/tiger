using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    public class Generator
    {
        public void Generate(Scope scope, ExecutableInfo info, Node ast)
        {
            var assembly_builder =
                AppDomain.CurrentDomain.DefineDynamicAssembly
                    (info.AssemblyName,
                    AssemblyBuilderAccess.RunAndSave,
                    info.FileDirectory);

            var module_builder =
                assembly_builder.DefineDynamicModule
                (info.AssemblyName.Name,
                info.OutputName);

            var type_builder = module_builder.DefineType("Program");

            var method_builder =
                type_builder.DefineMethod
                ("Main",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(int),
                new Type[] { });

            var il = method_builder.GetILGenerator();
            var ret = il.DeclareLocal(typeof(int));

            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc, ret);

            StandardLibrary.GenerateCodeFunctions(type_builder);

            var exception = il.DeclareLocal(typeof(Exception));
            il.BeginExceptionBlock();

            ast.GenerateCode(il, type_builder, module_builder);

            if (ast is ExpressionNode && !(((ExpressionNode)ast).ReturnType.Equals(VoidType.GetInstance)))
                il.Emit(OpCodes.Pop);

            il.BeginCatchBlock(typeof(Exception));
            il.Emit(OpCodes.Stloc, exception);
            il.Emit(OpCodes.Ldstr, "Exception of type ‘{0}’ was thrown.");
            il.Emit(OpCodes.Ldloc, exception);

            il.EmitCall(OpCodes.Callvirt, typeof(Exception).GetMethod("GetType"), null);
            il.EmitCall(OpCodes.Callvirt, exception.GetType().GetMethod("ToString"), null);
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string), typeof(object) }), null);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Stloc, ret);

            il.EndExceptionBlock();

            il.Emit(OpCodes.Ldloc, ret);
            il.Emit(OpCodes.Ret);
            type_builder.CreateType();
            assembly_builder.SetEntryPoint(method_builder);
            assembly_builder.Save(info.OutputName);
        }
    }
}
