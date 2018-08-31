using System.Collections.Generic;

namespace Tiger
{
    /// <summary>
    /// Entry point for semantics checking
    /// </summary>
    public class SemanticChecker : Checker
    {
        public void Check(Node ast, List<Error> errors, Scope scope)
        {
            scope.TypesDictionary.Add("int", IntType.GetInstance);
            scope.TypesDictionary.Add("string", StringType.GetInstance);
            StandardLibrary.DefineFunctions(scope);
            HasError = ast.CheckSemantics(scope, errors);
            if (HasError)
                errors.Sort();
        }
    }
}
