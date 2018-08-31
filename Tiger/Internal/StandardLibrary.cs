using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    public static class StandardLibrary
    {
        public static readonly List<string> FunctionIds = new List<string>
            { "print", "printline", "printi", "printiline", "getline", "chr", "size", "substring", "concat", "not", "exit", "ord" };
        public static List<string> UsedFunctions = new List<string>();

        private static List<StdLibFunction> Functions = new List<StdLibFunction>();
        public static void DefineFunctions(Scope scope)
        {
            var adaptor = new FunctionStdLibAdaptor();
            foreach (var identifier in FunctionIds)
            {
                var function = adaptor.CreateFunction(identifier);
                scope.FunctionDictionary.Add(identifier, function.GetFunctionNode());
                Functions.Add(function);
            }
        }

        public static void GenerateCodeFunctions(TypeBuilder type_builder)
        {
            for (int i = 0; i < Functions.Count; i++)
                if (UsedFunctions.Contains(FunctionIds[i]))
                    Functions[i].GenerateFunction(type_builder);
        }
    }
}
