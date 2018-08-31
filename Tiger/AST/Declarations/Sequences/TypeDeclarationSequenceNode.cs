using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Specifies the types declarations sequence
    /// </summary>
    public class TypeDeclarationSequenceNode : DeclarationSequenceNode
    {
        private List<TypeDeclarationNode> declarations;
        /// <summary>
        /// The list that contains all declared types in the sequence
        /// </summary>
        public List<TypeDeclarationNode> DeclaredTypes
        {
            get
            {
                if (declarations == null)
                {
                    declarations = new List<TypeDeclarationNode>();
                    for (int i = 0; i < ChildCount; i++)
                        declarations.Add(GetChild(i) as TypeDeclarationNode);
                }
                return declarations;
            }
        }
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //Define local variables
            var type_names = new HashSet<string>();
            var original_types = new List<RecordTypeDeclarationNode>();
            var redefinitions  = new List<TypeDeclarationNode>();

            foreach (var type in DeclaredTypes)
            {
                //Get redefinitions (or not) for record types and storages in local variables
                if(scope.GetType(type.Identifier, false) != null || !type_names.Add(type.Identifier))
                {
                    errors.Add(new TypeAlreadyFoundError(type.GetChild(0), type.Identifier));
                    if (type is RecordTypeDeclarationNode)
                        type.TypeDeclared = new RecordType(type.Identifier);
                    redefinitions.Add(type);
                }
                else if(type is RecordTypeDeclarationNode)
                {
                    original_types.Add((RecordTypeDeclarationNode) type);
                    type.TypeDeclared = new RecordType(type.Identifier);
                    scope.TypesDictionary.Add(type.Identifier, type.TypeDeclared);
                }
            }

            CircularDefinition.Analize(DeclaredTypes, scope, errors);

            //They must be separated because they are (in definition differents)
            foreach (var type in original_types)
                type.CheckSemantics(scope, errors);
            foreach (var type in redefinitions)
                type.CheckSemantics(scope, errors);

            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            foreach (var type in DeclaredTypes)
                if(type is RecordTypeDeclarationNode)
                {
                    var record = type as RecordTypeDeclarationNode;
                    ((RecordType)record.TypeDeclared).TypeBuilder = module_builder.DefineType(record.Identifier, TypeAttributes.Public);
                }

            foreach (var type in DeclaredTypes)
                if (type is RecordTypeDeclarationNode)
                {
                    var record = type as RecordTypeDeclarationNode;
                    record.TypeBuilder = ((RecordType)record.TypeDeclared).TypeBuilder;
                    record.GenerateCode(code_generator, type_builder, module_builder);
                }
                else
                    type.GenerateCode(code_generator, type_builder, module_builder);
        }
    }
}
