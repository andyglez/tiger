using System.Reflection.Emit;

namespace Tiger
{
    public abstract class StdLibFunction
    {
        protected FunctionDeclarationNode function;
        public abstract FunctionDeclarationNode GetFunctionNode();
        public abstract void GenerateFunction(TypeBuilder type_builder);
    }
}
