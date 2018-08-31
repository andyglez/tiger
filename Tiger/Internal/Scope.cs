using System.Collections.Generic;

namespace Tiger
{
    public class Scope
    {
        public Dictionary<string, FunctionDeclarationNode> FunctionDictionary = new Dictionary<string, FunctionDeclarationNode>();
        public Dictionary<string, VariableDeclarationNode> VariableDictionary = new Dictionary<string, VariableDeclarationNode>();
        public Dictionary<string, TigerType> TypesDictionary = new Dictionary<string, TigerType>();
        public Scope Parent { get; private set; }
        public Scope() { }
        public Scope(Scope parent) { Parent = parent; }

        public FunctionDeclarationNode GetFunction(string name, bool recursive = true)
        {
            FunctionDeclarationNode result;
            return FunctionDictionary.TryGetValue(name, out result) ? result :
                    (Parent != null) && recursive ? Parent.GetFunction(name) : null;
        }

        public VariableDeclarationNode GetVariable(string name, bool recursive = true)
        {
            VariableDeclarationNode result;
            return VariableDictionary.TryGetValue(name, out result) ? result :
                    (Parent != null) && recursive ? Parent.GetVariable(name) : null;
        }
        public TigerType GetType(string name, bool recursive = true)
        {
            TigerType result;
            return TypesDictionary.TryGetValue(name, out result) ? result :
                    (Parent != null) && recursive ? Parent.GetType(name) : null;
        }
    }
}
