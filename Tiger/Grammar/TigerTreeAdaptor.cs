using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace Tiger
{
    class TigerTreeAdaptor : CommonTreeAdaptor
    {
        public override object Create(IToken payload)
        {
            if (payload == null)
                return base.Create(payload);
            switch (payload.Type)
            {
                case TigerLexer.AND:
                    return new AndNode { Token = payload };
                case TigerLexer.OR:
                    return new OrNode { Token = payload };
                case TigerLexer.PLUS:
                    return new PlusNode { Token = payload };
                case TigerLexer.MINUS:
                    return new MinusNode { Token = payload };
                case TigerLexer.MINUS_EXPRESSION:
                    return new UnaryMinusNode { Token = payload };
                case TigerLexer.STAR:
                    return new StarNode { Token = payload };
                case TigerLexer.SLASH:
                    return new SlashNode { Token = payload };
                case TigerLexer.DIFF:
                    return new DistinctNode { Token = payload };
                case TigerLexer.EQUAL:
                    return new EqualNode { Token = payload };
                case TigerLexer.GETHAN:
                    return new GreaterEqualNode { Token = payload };
                case TigerLexer.GTHAN:
                    return new GreaterNode { Token = payload };
                case TigerLexer.LETHAN:
                    return new LowerEqualNode { Token = payload };
                case TigerLexer.LTHAN:
                    return new LowerNode { Token = payload };
                case TigerLexer.ASG:
                    return new AssignmentNode { Token = payload };
                case TigerLexer.ARRAY_CREATION:
                    return new ArrayCreationNode { Token = payload };
                case TigerLexer.FIELD_INIT_PAIR:
                    return new FieldInitPairNode { Token = payload };
                case TigerLexer.RECORD_CREATION:
                    return new RecordCreationNode { Token = payload };
                case TigerLexer.RECORDS_CREATION_FIELDS:
                    return new RecordCreationNode { Token = payload };
                case TigerLexer.ARRAY_ACCESS:
                    return new ArrayAccessNode { Token = payload };
                case TigerLexer.RECORD_FIELD_ACCESS:
                    return new RecordFieldAccessNode { Token = payload };
                case TigerLexer.VARIABLE_GET:
                    return new VariableNode { Token = payload };
                case TigerLexer.IF_THEN:
                    return new IfThenNode { Token = payload };
                case TigerLexer.IF_THEN_ELSE:
                    return new IfThenElseNode { Token = payload };
                case TigerLexer.WHILE:
                    return new WhileControlNode { Token = payload };
                case TigerLexer.FOR:
                    return new ForControlNode { Token = payload };
                case TigerLexer.BREAK:
                    return new BreakNode { Token = payload };
                case TigerLexer.INT:
                    return new IntConstantNode { Token = payload };
                case TigerLexer.STRING:
                    return new StringConstantNode { Token = payload };
                case TigerLexer.NIL:
                    return new NilConstantNode { Token = payload };
                case TigerLexer.EXPRESSION_LIST:
                    return new ExpressionListNode { Token = payload };
                case TigerLexer.FUNCTION_CALL:
                    return new FunctionCallNode { Token = payload };
                case TigerLexer.LET_IN_END:
                    return new LetNode { Token = payload };
                case TigerLexer.FUNCTION_DECLARATION_SEQUENCE:
                    return new FunctionDeclarationSequenceNode { Token = payload };
                case TigerLexer.TYPE_DECLARATION_SEQUENCE:
                    return new TypeDeclarationSequenceNode { Token = payload };
                case TigerLexer.VARIABLE_DECLARATION_SEQUENCE:
                    return new VarDeclarationSequenceNode { Token = payload };
                case TigerLexer.FUNCTION_DECLARATION:
                    return new FunctionDeclarationNode { Token = payload };
                case TigerLexer.ALIAS_DECLARATION:
                    return new AliasDeclarationNode { Token = payload };
                case TigerLexer.ARRAY_DECLARATION:
                    return new ArrayTypeDeclarationNode { Token = payload };
                case TigerLexer.RECORD_TYPE_DECLARATION:
                    return new RecordTypeDeclarationNode { Token = payload };
                case TigerLexer.FOR_VAR_DECLARATION:
                    return new ForVarDeclarationNode { Token = payload };
                case TigerLexer.FUNCTION_VAR_TYPE_DECLARATION:
                    return new FunctionParameterNode { Token = payload };
                case TigerLexer.LET_VAR_DECLARATION:
                    return new LetVarDeclarationNode { Token = payload };
                case TigerLexer.RECORD_VAR_DECLARATION:
                    return new RecordFieldDeclarationNode { Token = payload };
                default:
                    return base.Create(payload);
            }
        }
    }
}
