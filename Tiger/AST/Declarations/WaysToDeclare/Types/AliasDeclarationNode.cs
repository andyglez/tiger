using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides a type declaration that makes possible to set a new name to another type
    /// </summary>
    public class AliasDeclarationNode : TypeDeclarationNode
    {
        /// <summary>
        /// Gets the type identifier (or name) from whom type is aliasing
        /// </summary>
        public string BaseIdentifier { get { return Children[1].Text; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Check if the code is trying to overwrite int or string types definition
            if (Identifier.Equals(IntType.GetInstance.Id) || Identifier.Equals(StringType.GetInstance.Id))
                errors.Add(new TypeAlreadyDefinedStdLibError(GetChild(0), Identifier));
            if (scope.GetType(Identifier) != null && errors.Contains(new TypeAlreadyFoundError(GetChild(0), Identifier)))
                errors.Add(new TypeAlreadyFoundError(GetChild(0), Identifier));
            //Check if the original type actually exists
            TypeDeclared = scope.GetType(BaseIdentifier);
            if (TypeDeclared == null)
            {
                errors.Add(new TypeNotFoundError(GetChild(1), BaseIdentifier));
                TypeDeclared = VoidType.GetInstance;
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
    }
}
