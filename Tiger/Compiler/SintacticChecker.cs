using Antlr.Runtime;
using System.Collections.Generic;

namespace Tiger
{
    /// <summary>
    /// Entry point for sintactics checking
    /// </summary>
    class SintacticChecker : Checker
    {
        public Node Check(List<Error> errors, ICharStream source_code)
        {
            var lexer = new TigerLexer(source_code);
            lexer.Errors = new List<Error>();

            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new TigerParser(tokens);
            parser.Errors = new List<Error>();

            parser.TreeAdaptor = new TigerTreeAdaptor();
            var ast = parser.program().Tree as Node;

            foreach (var error in lexer.Errors)
                errors.Add(error);
            foreach (var error in parser.Errors)
                errors.Add(error);

            if (errors.Count > 0)
            {
                errors.Sort();
                HasError = true;
            }
            return ast;
        }
    }
}
