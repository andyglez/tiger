using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tiger
{
    public class TigerCompiler
    {
        public Owners Owners { get; private set; }
        public List<Error> Errors { get; private set; }
        public string GreetingMessage { get { return string.Format("Tiger Compiler Version {0}.{1}\n", Version.Major, Version.Minor) +
            string.Format("Copyright (C) {0}-{1} {2}\n", DateTime.Now.Year - 1, DateTime.Now.Year, Owners); } }
        public Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public TigerCompiler(params Student[] owners)
        {
            Owners = new Owners(owners);
            Errors = new List<Error>();
        }

        public bool Compiles(ExecutableInfo exe)
        {
            var file_check = new FileChecker();
            var source_code = file_check.Check(Errors, exe.PathInput);
            if (file_check.HasError) return false;
            var sintactic = new SintacticChecker();
            var ast = sintactic.Check(Errors, source_code);
            if (sintactic.HasError) return false;
            var scope = new Scope();
            var semantic = new SemanticChecker();
            semantic.Check(ast, Errors, scope);
            if (semantic.HasError) return false;
            var generator = new Generator();
            generator.Generate(scope, exe, ast);
            return true;
        }
    }
}
