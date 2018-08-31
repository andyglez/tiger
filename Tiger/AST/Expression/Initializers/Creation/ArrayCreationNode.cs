using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Tiger
{
    /// <summary>
    /// Represents the way to create an instance of an array 
    /// </summary>
    public class ArrayCreationNode : CreationNode
    {
        /// <summary>
        /// Gets the size expression initializer
        /// </summary>
        public ExpressionNode Size { get { return Children[1] as ExpressionNode; } }
        /// <summary>
        /// Gets the construction expression or the type expression
        /// </summary>
        public ExpressionNode OfExpression { get { return Children[2] as ExpressionNode; } }
        
        public override bool CheckSemantics(Scope scope, List<Error> errors)
        {
            //By default is bad definition
            ReturnType = BadType.GetInstance;
            Size.CheckSemantics(scope, errors);
            if (!Size.ReturnType.Equals(IntType.GetInstance))
                errors.Add(new ArraySizeError(Size, Size.ReturnType.Id));
            OfExpression.CheckSemantics(scope, errors);
            //The error names speaks for themselves
            var type = scope.GetType(Identifier);
            if (type == null)
                errors.Add(new TypeNotFoundError(GetChild(0), Identifier));
            else if (!(type is ArrayType))
                errors.Add(new MustBeArrayError(GetChild(0), type.Id));
            else if (!OfExpression.ReturnType.Equals(((ArrayType)type).ElementsType))
                errors.Add(new ArrayCreationMismatchError(OfExpression, OfExpression.ReturnType.Id, ((ArrayType)type).ElementsType.Id));
            else
                ReturnType = type;
            return errors.Count > 0;
        }

        public override void GenerateCode(ILGenerator code_generator, TypeBuilder type_builder, ModuleBuilder module_builder)
        {
            Type elementType = ((ArrayType)ReturnType).ElementsType.BaseType;

            code_generator.BeginScope();
            Label forStart = code_generator.DefineLabel();
            Label forEnd = code_generator.DefineLabel();
            LocalBuilder lengthBuilder = code_generator.DeclareLocal(typeof(int));
            LocalBuilder i = code_generator.DeclareLocal(typeof(int));
            code_generator.Emit(OpCodes.Ldc_I4_M1);
            code_generator.Emit(OpCodes.Stloc, i);

            Size.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Stloc, lengthBuilder);
            code_generator.Emit(OpCodes.Ldloc, lengthBuilder);
            code_generator.Emit(OpCodes.Newarr, elementType);

            code_generator.MarkLabel(forStart);

            // i++
            code_generator.Emit(OpCodes.Dup);
            code_generator.Emit(OpCodes.Ldloc, i);
            code_generator.Emit(OpCodes.Ldc_I4_1);
            code_generator.Emit(OpCodes.Add);
            code_generator.Emit(OpCodes.Stloc, i);
            code_generator.Emit(OpCodes.Ldloc, i);

            // if i >= lengthBuilder goto END
            code_generator.Emit(OpCodes.Ldloc, lengthBuilder);
            code_generator.Emit(OpCodes.Bge, forEnd);

            // Stack:   arrayRef / i / obj / Stelem
            code_generator.Emit(OpCodes.Ldloc, i);
            OfExpression.GenerateCode(code_generator, type_builder, module_builder);
            code_generator.Emit(OpCodes.Stelem, elementType);

            code_generator.Emit(OpCodes.Br, forStart);

            code_generator.MarkLabel(forEnd);

            code_generator.Emit(OpCodes.Pop);
            code_generator.EndScope();
        }
    }
}
