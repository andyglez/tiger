namespace Tiger
{
    public class FunctionStdLibAdaptor
    {
        public StdLibFunction CreateFunction(string identifier)
        {
            switch (identifier)
            {
                case "print":
                    return new PrintFunction();
                case "printline":
                    return new PrintLineFunction();
                case "printi":
                    return new PrintiFunction();
                case "printiline":
                    return new PrintiLineFunction();
                case "getline":
                    return new GetLineFunction();
                case "chr":
                    return new ChrFunction();
                case "size":
                    return new SizeFunction();
                case "substring":
                    return new SubstringFunction();
                case "concat":
                    return new ConcatFunction();
                case "not":
                    return new NotFunction();
                case "exit":
                    return new ExitFunction();
                case "ord":
                    return new OrdFunction();
            }
            return null;
        }
    }
}
