using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger
{
    public class ForVariableMismatchReturnError : VariableMismatchReturnError
    {
        public ForVariableMismatchReturnError(ITree tree, string id, string expression_return_type)
            : base(tree, string.Format("For loop: {0}", id), expression_return_type, IntType.GetInstance.Id) { }
    }
}
