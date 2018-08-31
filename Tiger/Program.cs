using System;

namespace Tiger
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new TigerCompiler(
                new Student("Andy Gonzalez", "C-412"),
                new Student("Yamile Reynoso", "C-412"));
            Console.WriteLine(compiler.GreetingMessage);

            if (args == null || args.Length == 0)
                return;
            string input_path = args[0];
            if (compiler.Compiles(new ExecutableInfo(input_path, "1.0")))
                Environment.Exit(0);
            foreach (var error in compiler.Errors)
                Console.WriteLine("({0},{1}): {2}", error.Line, error.Column, error.Message);
            Environment.Exit(1);
        }
    }
}
