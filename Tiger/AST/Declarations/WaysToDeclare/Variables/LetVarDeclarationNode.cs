using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Provides definition for a variable declaration
    /// </summary>
    public class LetVarDeclarationNode : VariableDeclarationNode
    {
        /// <summary>
        /// If defined gets the type identifier declared for this variable
        /// </summary>
        public string TypeId { get { return ChildCount == 3 ? Children[2].Text : null; } }

        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            InitializeVar.CheckSemantics(scope, errors);
            TypeDeclared = VoidType.GetInstance;

            //If type of the variable is specified / else get type from the right side
            if (TypeId != null)
            {
                var type = scope.GetType(TypeId);
                if (type == null)
                    errors.Add(new TypeNotFoundError(GetChild(2), TypeId));
                else
                {
                    if (!InitializeVar.ReturnType.Equals(type))
                        errors.Add(new VariableMismatchReturnError(InitializeVar, Identifier, TypeId, InitializeVar.ReturnType.Id));
                    TypeDeclared = type;
                }
            }
            else
            {
                if (InitializeVar.ReturnType.Equals(VoidType.GetInstance))
                    errors.Add(new InferError(InitializeVar));
                else if (InitializeVar is NilConstantNode)
                    errors.Add(new InferError(InitializeVar));
                else
                    TypeDeclared = InitializeVar.ReturnType;
            }
            //Check for functions or variables with the same name
            var function_declaration = scope.GetFunction(Identifier);
            var variable_declaration = scope.GetVariable(Identifier);
            if (function_declaration != null)
                errors.Add(new FunctionAlreadyFoundError(GetChild(0), Identifier));
            if (variable_declaration != null)
                errors.Add(new VariableAlreadyFoundError(GetChild(0), Identifier));
            else
                scope.VariableDictionary.Add(Identifier, this);
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            VariableBuilder = type_builder.DefineField(Identifier, TypeDeclared.BaseType, FieldAttributes.Static | FieldAttributes.Public);
            LocalBuilder = code_generator.DeclareLocal(VariableBuilder.FieldType);
            
            code_generator.Emit(OpCodes.Ldsfld, VariableBuilder);
            code_generator.Emit(OpCodes.Stloc, LocalBuilder);
            InitializeVar.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Stsfld, VariableBuilder);
        }

        public void RestoreVariable(ILGenerator code_generator)
        {
            code_generator.Emit(OpCodes.Ldloc, LocalBuilder);
            code_generator.Emit(OpCodes.Stsfld, VariableBuilder);
        }

        public override void GetValue(ILGenerator code_generator)
        {
            code_generator.Emit(OpCodes.Ldsfld, VariableBuilder);
        }
    }
}
