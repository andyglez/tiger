grammar Tiger;

options
{
    output = AST;
    ASTLabelType = CommonTree;
    language = CSharp3;
}

tokens
{
    //Tiger default keywords
    ARRAY   = 'array';
    BREAK   = 'break';
    DO      = 'do';
    ELSE    = 'else';
    END     = 'end';
    FOR     = 'for';
    FUNCTION= 'function';
    IF      = 'if';
    IN      = 'in';
    LET     = 'let';
    NIL     = 'nil';
    OF      = 'of';
    THEN    = 'then';
    TO      = 'to';
    TYPE    = 'type';
    VAR     = 'var';
    WHILE   = 'while';

    //Tiger symbols
    DOT     = '.';
    COMMA   = ',';
    DDOT    = ':';
    SEMI    = ';';
    LBRACK  = '[';
    RBRACK  = ']';
    LPAR    = '(';
    RPAR    = ')';
    LKEY    = '{';
    RKEY    = '}';

    //Tiger operators symbols
    PLUS    = '+';
    MINUS   = '-';
    STAR    = '*';
    SLASH   = '/';
    AND     = '&';
    OR      = '|';
    EQUAL   = '=';
    DIFF    = '<>';
    LTHAN   = '<';
    LETHAN  = '<=';
    GTHAN   = '>';
    GETHAN  = '>=';
    ASG     = ':=';

    //AST token node
    //expressions (while and break are reusable)
    MINUS_EXPRESSION;
    EXPRESSION_LIST;
    VARIABLE_GET;
    FUNCTION_CALL;
    RECORD_CREATION;
    RECORD_FIELD_ACCESS;
    ARRAY_CREATION;
    ARRAY_ACCESS;
    FIELD_INIT_PAIR; 
    IF_THEN;
    IF_THEN_ELSE;
    FOR;
    LET_IN_END;

    //declaration_list
    //types
    TYPE_DECLARATION_SEQUENCE;
    ALIAS_DECLARATION;
    ARRAY_DECLARATION;
    RECORD_TYPE_DECLARATION;
    FUNCTION_VAR_TYPE_DECLARATION;

    //vars
    VARIABLE_DECLARATION_SEQUENCE;
    LET_VAR_DECLARATION;
    FOR_VAR_DECLARATION;
    RECORD_VAR_DECLARATION;

    //function
    FUNCTION_DECLARATION_SEQUENCE;
    FUNCTION_DECLARATION;

    //Properties that will exist on the current scope
    LET_DECLARATIONS;            
    FUNCTION_ARGS_DECLARATIONS;
    FUNCTION_CALL_ARGS;
    RECORDS_FIELDS_DECLARATIONS;
    RECORDS_CREATION_FIELDS;
}

// ------- LEXER OPTIONS: -----------------------------------------
@lexer::header
{
using System;
}

@lexer::namespace { Tiger }

@lexer::modifier { internal }

@lexer::ctorModifier { public }

@lexer::ctor
{
public TigerLexer(ICharStream stream,List<Error> errors) : this(stream)
{
    Errors = errors;
}    
}
@lexer::members 
{ 
public override string GetErrorMessage( RecognitionException e, string[] token)
{
    string message = base.GetErrorMessage(e, token);
    Errors.Add(new Error(e,message));
    return message;
}
public int HIDDEN{get{return Hidden;}}
public List<Error> Errors { get; set; }
}


// ------- PARSER OPTIONS: ----------------------------------------
@parser::header 
{ 
using System;
}

@parser::namespace { Tiger }

@parser::modifier { internal }

@parser::ctorModifier { public }

@parser::ctor
{
public TigerParser(ITokenStream stream, List<Error> errors)
        : this(stream)
{
    Errors = errors;
}
}
@parser::members 
{ 
public override string GetErrorMessage( RecognitionException e, string[] token)
{
    string message = base.GetErrorMessage(e, token);
    Errors.Add(new Error(e,message));
    return message;
}
public List<Error> Errors { get; set; }
}


//---------------------- DEFAULT GENERATED TOKENS --------------------

fragment LETTER : 'a'..'z'|'A'..'Z';
fragment DIGIT  : '0' .. '9';

ID  :	LETTER (LETTER|DIGIT|'_')*;

INT :	DIGIT+;

COMMENT
    :   '//' ~('\n'|'\r')* '\r'? '\n' {$channel=HIDDEN;}
    |   '/*' ( options {greedy=false;} : . )* '*/' {$channel=HIDDEN;}
    ;

WS  :   ( ' '
        | '\t'
        | '\r'
        | '\n'
        ) {$channel=HIDDEN;}
    ;

fragment BACKSLASH : '\\';
fragment QUOTE     : '"';

STRING
    :  QUOTE ( ESC_SEQ | ~(BACKSLASH|QUOTE) )* QUOTE
    ;

fragment
HEX_DIGIT : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
ESC_SEQ
    :   BACKSLASH
     ('n'
     |'r'
     |'t'
     |'"'
     |BACKSLASH
     |'^'('@'|'A'..'Z'|'['|'\\'|']'|'^'|'_')
     |DIGIT DIGIT DIGIT
     |(WS)+ BACKSLASH)
    ;

//--------------------- GRAMMAR --------------------------

public program
    : expression EOF!;

expression
    : or;

or
    : and (OR^ and)*;

and
    : comparison (AND^ comparison)*;

comparison
    : arithmetic (relational_operation^ arithmetic)?;

arithmetic
    : result (plus_operation^ result)*;

result
    : value (star_operation^ value)*;

//--- OPERATIONS ------
relational_operation
    : EQUAL | DIFF | LTHAN | LETHAN | GTHAN | GETHAN;

plus_operation
    : PLUS | MINUS;

star_operation
    : STAR | SLASH;

//--- VALUES -----------------------------

value
    : constants_literals
    | flow_expression
    | MINUS value -> ^(MINUS_EXPRESSION value)
    | (ID LBRACK expression RBRACK OF) => array_creation
    | (lvalue ASG) => assignment
    | access_expression (access^)*;

constants_literals
    : INT^ | STRING^ | NIL^;

access
    : LBRACK access_exp=expression RBRACK -> ^(ARRAY_ACCESS $access_exp)
    | DOT field_name=ID -> ^(RECORD_FIELD_ACCESS $field_name);


// --- FLOW CONTROL ------
flow_expression
    : if_expression
    | while_expression
    | for_expression
    | break_expression;

// --- IF CONTROL --------
if_expression
    : (IF expression THEN expression ELSE) => if_then_else_expression
    | if_then_expression;

if_then_else_expression
    : IF condition=expression THEN then_expression=expression ELSE else_expression=expression 
    -> ^(IF_THEN_ELSE $condition $then_expression $else_expression);

if_then_expression
    : IF condition=expression THEN body=expression 
    -> ^(IF_THEN $condition $body);

// --- FOR/WHILE CONTROL -------
while_expression
    : WHILE^ expression DO! expression;

for_expression
    : FOR var_name=ID ASG init_var=expression TO end_loop_condition=expression DO body=expression
    -> ^(FOR ^(FOR_VAR_DECLARATION $var_name $init_var) $end_loop_condition $body);

break_expression
    : BREAK^;


// --- CREATIONS (types and arrays) -------
array_creation
    : type_name=ID LBRACK size=expression RBRACK OF init_val=expression
    -> ^(ARRAY_CREATION $type_name $size $init_val);

type_creation
    : type_name=ID LKEY field_list? RKEY
    -> ^(RECORD_CREATION $type_name ^(RECORDS_CREATION_FIELDS field_list?));

field_list
    : single_field_list (COMMA single_field_list)* -> single_field_list+; 

single_field_list
    : field_id=ID EQUAL val=expression -> ^(FIELD_INIT_PAIR $field_id $val);
assignment
    : lvalue ASG^ expression;

lvalue
    : access_expression (access^)+
    | variable;

variable
    : name=ID -> ^(VARIABLE_GET $name);

access_expression
    : (ID LKEY) => type_creation
    | (ID LPAR) => function_call
    | let_in_end_expression
    | expressions_lists
    | variable;

expressions_lists
    : LPAR expression_sequence? RPAR -> ^(EXPRESSION_LIST expression_sequence?);

expression_sequence
    : expression (SEMI expression)* 
    -> expression+ ;

// --- FUNCTION CALLS -----------
function_call
    : id_function=ID LPAR args_list? RPAR
    -> ^(FUNCTION_CALL $id_function ^(FUNCTION_CALL_ARGS args_list?));

args_list
    : expression (COMMA expression)* -> expression+;

let_in_end_expression
    :  LET declaration_list IN expression_sequence? END
    -> ^(LET_IN_END declaration_list ^(EXPRESSION_LIST expression_sequence?));

// ------------- DECLARATIONS ----------------------------------
declaration_list
    : declaration_sequence+
    -> ^(LET_DECLARATIONS declaration_sequence+);

declaration_sequence
    : var_declaration
    | type_declaration
    | function_declaration;

var_declaration
    : single_var_declaration+
    -> ^(VARIABLE_DECLARATION_SEQUENCE single_var_declaration+);

type_declaration
    : single_type_declaration+
    -> ^(TYPE_DECLARATION_SEQUENCE single_type_declaration+);

function_declaration
    : single_function_declaration+
    -> ^(FUNCTION_DECLARATION_SEQUENCE single_function_declaration+);

// -------- VAR DECLARATION --------------
single_var_declaration
    : VAR name=ID 
        (ASG init_var=expression -> ^(LET_VAR_DECLARATION $name $init_var)
        | DDOT type_name=ID ASG init_var=expression -> ^(LET_VAR_DECLARATION $name $init_var $type_name));

// ------- TYPE DECLARATION --------------
single_type_declaration
    : TYPE name=ID EQUAL
        (base_type=ID -> ^(ALIAS_DECLARATION $name $base_type)
        | ARRAY OF type_elements=ID -> ^(ARRAY_DECLARATION $name $type_elements)
        | LKEY field_sequence? RKEY -> ^(RECORD_TYPE_DECLARATION $name ^(RECORDS_FIELDS_DECLARATIONS field_sequence?)));

field_sequence
    : single_field (COMMA single_field)* -> single_field+;

single_field
    : var_name=ID DDOT type_name=ID -> ^(RECORD_VAR_DECLARATION $var_name $type_name);

// ------ FUNCTION DECLARATION -----------
single_function_declaration
    : FUNCTION name=ID LPAR args_declaration? RPAR
        (DDOT return_type=ID EQUAL body=expression -> ^(FUNCTION_DECLARATION $name ^(FUNCTION_ARGS_DECLARATIONS args_declaration?) $body $return_type)
        | EQUAL body=expression -> ^(FUNCTION_DECLARATION $name ^(FUNCTION_ARGS_DECLARATIONS args_declaration?) $body));

args_declaration
    : single_arg_declaration (COMMA single_arg_declaration)* -> single_arg_declaration+;

single_arg_declaration
    : var_name=ID DDOT type_name=ID -> ^(FUNCTION_VAR_TYPE_DECLARATION $var_name $type_name);
