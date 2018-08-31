using Antlr.Runtime.Tree;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Main abstract class for the AST
    /// </summary>
    public abstract class Node : CommonTree
    {
        /// <summary>
        /// This method provides for a tool to the semantics analizers
        /// </summary>
        /// <param name="scope">Defines the context where the statement lives</param>
        /// <param name="errors">Contains the list of errors found, inside it could be added some errors</param>
        /// <returns>If found and error then returns true</returns>
        public abstract bool CheckSemantics(Scope scope, List<Error> errors);
        /// <summary>
        /// This method provides for a tool to generate code in runtime
        /// </summary>
        /// <param name="code_generator">It is an IL generator</param>
        /// <param name="type_builder">The Type Builder for types constructions</param>
        /// <param name="module_builder">The Module Builder for modules constructions</param>
        public abstract void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder);
    }
}
