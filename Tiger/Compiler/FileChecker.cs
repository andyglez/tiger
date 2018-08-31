using Antlr.Runtime;
using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;

namespace Tiger
{
    /// <summary>
    /// Checks for file existance
    /// </summary>
    class FileChecker : Checker
    {
        public ICharStream Check(List<Error> errors, string path)
        {
            ICharStream ret;
            try { ret = new ANTLRFileStream(path); }
            catch (Exception)
            {
                errors.Add(new Error(new CommonTree { Line = 0, CharPositionInLine = 0 }, "File {0} can't be found", path));
                HasError = true;
                return null;
            }
            return ret;
        }
    }
}
