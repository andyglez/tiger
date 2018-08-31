using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides a type declaration for an array
    /// </summary>
    public class ArrayTypeDeclarationNode : TypeDeclarationNode
    {
        /// <summary>
        /// Gets the type identifier for the elements of the array
        /// </summary>
        public string ElementsTypeIdentifier { get { return Children[1].Text; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Analize if the type of the elements of the array actually exists
            var type_of_elements = scope.GetType(ElementsTypeIdentifier);
            if (type_of_elements == null)
            {
                type_of_elements = new ArrayType(NilType.GetInstance, Identifier);
                errors.Add(new TypeNotFoundError(GetChild(1), ElementsTypeIdentifier));
            }
            TypeDeclared = new ArrayType(type_of_elements, Identifier);
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
    }
}
