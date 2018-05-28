using System;

using PasC.Models;

namespace PasC.Modules
{
    class Parser
    {
        // Resources
        private static int ERROR_COUNT;


        // Lexer Resources
        private static Token TOKEN;




        // Parser Control
        public static void Set()
        {
            Advance();

            Prog();
        }




        // Errors
        private static void SyntacticError(String lexeme)
        {
            ERROR_COUNT++;

            Console.WriteLine("\n[SYNTACTIC ERROR]: Expected {0} but received \"{1}\" on line {2} and column {3}.", lexeme, TOKEN.Lexeme, TOKEN.Row, TOKEN.Column);

            if (ERROR_COUNT == 5)
            {
                Console.WriteLine("\n[SYNTACTIC ERROR]: Many syntactic errors, aborting execution.");

                Console.WriteLine("\nPress ENTER to continue...");

                Console.ReadKey();

                Environment.Exit(1);
            }
        }




        // Token Handlers
        private static Tag GetTag()
        {
            return TOKEN.GetTag();
        }

        private static void Advance()
        {
            TOKEN = Lexer.NextToken();

            Console.WriteLine("[DEBUG]" + TOKEN.ToString());
        }

        private static bool Eat(Tag tag)
        {
            if (GetTag() == tag)
            {
                Advance();

                return true;
            }
            else
            {
                return false;
            }
        }




        // Todas as decisoes sao baseadas na tabela preditiva

        // prog -> "program" "id" body
        private static void Prog()
        {
            if (Eat(Tag.KW_PROGRAM))
            {
                if (!Eat(Tag.ID))
                {
                    SyntacticError("\"<ID>\"");
                    return;
                }

                Body();
            }
            else
            {
                SyntacticError("\"program\"");
                return;
            }
        }


        //body -> decl-list "{" stmt-list "}"
        private static void Body()
        {
            Decl_List();

            if (Eat(Tag.SMB_OBC))
            {
                Stmt_List();

                if (!Eat(Tag.SMB_CBC))
                {
                    SyntacticError("\"}}\"");
                    return;
                }
            }
        }


        // decl-list -> decl ";" decl-list | ε
        private static void Decl_List()
        {
            // ε -> "{"
            if (GetTag() == Tag.SMB_OBC)
            {
                return;
            }

            // decl ";" decl-list
            else
            {
                Decl();

                if (!Eat(Tag.SMB_SEM))
                {
                    SyntacticError("\";\"");
                    return;
                }

                Decl_List();
            }
        }


        // decl -> type id-list
        private static void Decl()
        {
            Type();

            Id_List();
        }


        // type -> "num" "char"
        private static void Type()
        {
            if (!Eat(Tag.KW_NUM) && !Eat(Tag.KW_CHAR))
            {
                SyntacticError("\"num\", \"char\"");
                return;
            }
        }


        // id-list -> "id" id-list'
        private static void Id_List()
        {
            if (!Eat(Tag.ID))
            {
                SyntacticError("\"<ID>\"");
                return;
            }

            Id_List2();
        }


        // id-list' -> "," id-list | ε
        private static void Id_List2()
        {
            // ε -> ";"
            if (GetTag() == Tag.SMB_SEM)
            {
                return;
            }

            // "," id-list
            else
            {
                if (!Eat(Tag.SMB_COM))
                {
                    SyntacticError("\",\"");
                    return;
                }

                Id_List();
            }
        }


        // stmt-list -> stmt ";" stmt-list | ε
        private static void Stmt_List()
        {
            // ε -> "}"
            if (GetTag() == Tag.SMB_CBC)
            {
                return;
            }

            // stmt ";" stmt-list
            else
            {
                Stmt();

                if (!Eat(Tag.SMB_SEM))
                {
                    SyntacticError("\";\"");
                    return;
                }

                if (GetTag() == Tag.ID || GetTag() == Tag.KW_IF || GetTag() == Tag.KW_WHILE ||
                   GetTag() == Tag.KW_READ || GetTag() == Tag.KW_WRITE)
                {
                    Stmt_List();
                }
                else
                {
                    return;
                }
            }
        }


        // stmt -> assign-stmt | if-stmt | while-stmt | read-stmt | write-stmt
        private static void Stmt()
        {
            // assign-stmt
            if (GetTag() == Tag.ID)
            {
                Assign_Stmt();
            }

            // if-stmt 
            if (GetTag() == Tag.KW_IF)
            {
                If_Stmt();
            }

            // while-stmt
            if (GetTag() == Tag.KW_WHILE)
            {
                While_Stmt();
            }

            // read-stmt
            if (GetTag() == Tag.KW_READ)
            {
                Read_Stmt();
            }

            // write-stmt
            if (GetTag() == Tag.KW_WRITE)
            {
                Write_Stmt();
            }
        }


        // assign-stmt -> "id" "=" simple_expr
        private static void Assign_Stmt()
        {
            if (!Eat(Tag.ID))
            {
                SyntacticError("\"<ID>\"");
                return;
            }

            if (!Eat(Tag.OP_ASS))
            {
                SyntacticError("\"=\"");
                return;
            }

            Simple_Expr();
        }


        // if-stmt -> "if" "(" condition ")" "{" stmt-list "}" if-stmt'
        private static void If_Stmt()
        {
            if (!Eat(Tag.KW_IF))
            {
                SyntacticError("\"if\"");
                return;
            }

            if (!Eat(Tag.SMB_OPA))
            {
                SyntacticError("\"(\"");
                return;
            }

            Condition();

            if (!Eat(Tag.SMB_CPA))
            {
                SyntacticError("\")\"");
                return;
            }

            if (!Eat(Tag.SMB_OBC))
            {
                SyntacticError("\"{\"");
                return;
            }

            Stmt_List();

            if (!Eat(Tag.SMB_CBC))
            {
                SyntacticError("\"}\"");
                return;
            }

            If_Stmt2();
        }


        // if-stmt' -> "else" "{" stmt-list "}" | ε
        private static void If_Stmt2()
        {
            // ε -> ";"
            if (GetTag() == Tag.SMB_SEM)
            {
                return;
            }

            // "else" "{" stmt-list "}"
            else
            {
                if (!Eat(Tag.KW_ELSE))
                {
                    SyntacticError("\"else\"");
                    return;
                }

                if (!Eat(Tag.SMB_OBC))
                {
                    SyntacticError("\"{\"");
                    return;
                }

                Stmt_List();

                if (!Eat(Tag.SMB_CBC))
                {
                    SyntacticError("\"}\"");
                    return;
                }
            }
        }


        // condition -> expression
        private static void Condition()
        {
            Expression();
        }


        // while-stmt -> stmt-prefix "{" stmt-list "}"
        private static void While_Stmt()
        {
            Stmt_Prefix();

            if (!Eat(Tag.SMB_OBC))
            {
                SyntacticError("\"{\"");
                return;
            }

            Stmt_List();

            if (!Eat(Tag.SMB_CBC))
            {
                SyntacticError("\"}\"");
                return;
            }
        }


        // stmt-prefix -> "while" "(" condition ")"
        private static void Stmt_Prefix()
        {
            if (!Eat(Tag.KW_WHILE))
            {
                SyntacticError("\"while\"");
                return;
            }

            if (!Eat(Tag.SMB_OPA))
            {
                SyntacticError("\"(\"");
                return;
            }

            Condition();

            if (!Eat(Tag.SMB_CPA))
            {
                SyntacticError("\")\"");
                return;
            }
        }


        // read-stmt -> "read" "id"
        private static void Read_Stmt()
        {
            if (!Eat(Tag.KW_READ))
            {
                SyntacticError("\"read\"");
                return;
            }

            if (!Eat(Tag.ID))
            {
                SyntacticError("\"<ID>\"");
                return;
            }
        }


        // write-stmt -> "write" writable
        private static void Write_Stmt()
        {
            if (!Eat(Tag.KW_WRITE))
            {
                SyntacticError("\"write\"");
                return;
            }

            Writable();
        }


        // writable -> simple-expr | "literal"
        private static void Writable()
        {
            // simple-expr
            if (GetTag() == Tag.ID || GetTag() == Tag.CON_NUM || GetTag() == Tag.CON_CHAR || GetTag() == Tag.SMB_OPA || GetTag() == Tag.KW_NOT)
            {
                Simple_Expr();
            }

            // "literal"
            else if (!Eat(Tag.LIT))
            {
                SyntacticError("\"<LITERAL>\"");
                return;
            }
        }


        // expression -> simple-expr expression'
        private static void Expression()
        {
            Simple_Expr();

            Expression2();
        }


        // expression -> relop simple-expr | ε
        private static void Expression2()
        {
            // ε -> ")"
            if (GetTag() == Tag.SMB_CPA)
            {
                return;
            }

            // relop simple-expr
            else
            {
                RelOp();

                Simple_Expr();
            }
        }


        // simple-expr -> term simple-expr'
        private static void Simple_Expr()
        {
            Term();

            Simple_Expr2();
        }


        // simple-expr' -> addop term simple-expr' | ε
        private static void Simple_Expr2()
        {
            // ε -> ";"
            if (GetTag() == Tag.SMB_SEM)
            {
                return;
            }

            // addop term simple-expr'
            else
            {
                if (GetTag() == Tag.OP_AD || GetTag() == Tag.OP_MIN || GetTag() == Tag.KW_OR)
                {
                    AddOp();

                    Term();

                    Simple_Expr2();
                }
                else
                {
                    SyntacticError("\"+\", \"-\", \"or\"");
                    return;
                }
            }
        }


        // Term -> factor-a term'
        private static void Term()
        {
            Factor_A();

            Term2();
        }


        // term' -> mulop factor-a term' | ε
        private static void Term2()
        {
            // ε -> "+" || "-" || OR || ";"
            if (GetTag() == Tag.OP_AD || GetTag() == Tag.OP_MIN || GetTag() == Tag.KW_OR || GetTag() == Tag.SMB_SEM)
            {
                return;
            }

            // mulop factor-a term'
            else
            {
                if (GetTag() == Tag.OP_MUL || GetTag() == Tag.OP_DIV || GetTag() == Tag.KW_AND)
                {
                    MulOp();

                    Factor_A();

                    Term2();
                }
                else
                {
                    SyntacticError("\"*\", \"/\", \"and\"");
                    return;
                }
            }
        }


        // factor-a -> factor | "not" factor
        private static void Factor_A()
        {
            // factor
            if (GetTag() == Tag.ID || GetTag() == Tag.CON_NUM || GetTag() == Tag.CON_CHAR || GetTag() == Tag.SMB_OPA || GetTag() == Tag.KW_NOT)
            {
                // "not" factor
                if (Eat(Tag.KW_NOT))
                {
                    Factor();
                }
                else
                {
                    Factor();
                }

                return;
            }
            else
            {
                SyntacticError("\"<ID>\", \"<DIGIT>\", \"<LETTER>\", \"not\"");

                return;
            }
        }


        // factor -> "id" | constant | "(" expression ")"
        private static void Factor()
        {
            // "id"
            if (GetTag() == Tag.ID)
            {
                Eat(Tag.ID);

                return;
            }

            // constant
            if (GetTag() == Tag.CON_NUM || GetTag() == Tag.CON_CHAR)
            {
                Constant();

                return;
            }

            // "(" expression ")"
            if (Eat(Tag.SMB_OPA))
            {
                Expression();

                if (!Eat(Tag.SMB_CPA))
                {
                    SyntacticError("\")\"");
                    return;
                }
            }
            else
            {
                SyntacticError("\"(\"");
                return;
            }
        }


        // relop -> "==" | ">" | ">=" | "<" | "<=" | "!="
        private static void RelOp()
        {
            if (!Eat(Tag.OP_EQ) && !Eat(Tag.OP_GT) && !Eat(Tag.OP_GE) && !Eat(Tag.OP_LT) && !Eat(Tag.OP_LE) && !Eat(Tag.OP_NE))
            {
                SyntacticError("\"==\", \">\", \">=\", \"<\", \"<=\", \"!=\"");
                return;
            }
        }


        // addop -> "+" | "-" | "or"
        private static void AddOp()
        {
            if (!Eat(Tag.OP_AD) && !Eat(Tag.OP_MIN) && !Eat(Tag.KW_OR))
            {
                SyntacticError("\"+\", \"-\", \"or\"");
                return;
            }
        }


        // mulop -> "*" | "/" | "and"
        private static void MulOp()
        {
            if (!Eat(Tag.OP_MUL) && !Eat(Tag.OP_DIV) && !Eat(Tag.KW_AND))
            {
                SyntacticError("\"*\", \"/\", \"and\"");
                return;
            }
        }


        // constant -> "num_const" | "char_const"
        private static void Constant()
        {
            if (!Eat(Tag.CON_NUM) && !Eat(Tag.CON_CHAR))
            {
                SyntacticError("\"<CON_NUM>\", \"<CON_CHAR>\"");

                return;
            }
        }
    }
}
