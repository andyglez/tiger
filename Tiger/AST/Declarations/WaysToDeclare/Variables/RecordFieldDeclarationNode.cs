using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides definition for a record field variable
    /// </summary>
    public class RecordFieldDeclarationNode : VariableDeclarationNode
    {
        /// <summary>
        /// Gets the type name of the field
        /// </summary>
        public string TypeId { get { return Children[1].Text; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            TypeDeclared = scope.GetType(TypeId);
            if (TypeDeclared == null)
            {
                TypeDeclared = BadType.GetInstance;
                errors.Add(new TypeNotFoundError(GetChild(1), TypeId));
            }
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder) { return; }
        public override void GetValue(ILGenerator code_generator) { return; }
    }
}
