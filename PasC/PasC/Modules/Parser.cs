using System;

using PasC.Models;

namespace PasC.Modules
{
    class Parser
    {
		// Lexer
        private Lexer lexer;
        private Token token;




		// Lexer<->Parser
        public Parser(Lexer lexer)
        {

        }


		// Errors
        public void SyntacticError(String message)
        {
            Console.WriteLine("\n[SYNTACTIC ERROR]: " + message);
        }




		// Token Handlers
        public void Advance()
        {
            //token = lexer.getToken();
            Console.WriteLine("[DEBUG]" + token.ToString());
        }

        // Verificação de token esperado
        public bool Eat(Tag tag)
        {
            if (token.GetTag() == tag)
            {
                Advance();

                return true;
            }
            else
            {
                return false;
            }
        }




        // Classes
        // Todas as decisoes sao baseadas na tabela preditiva
        public void Prog()
        {

        }

        public void Body()
        {

        }

        public void Decl_List()
        {

        }

        public void Decl()
        {

        }

        public void Type()
        {

        }

        public void Id_List()
        {

        }

        // id-list'
        public void Id_List2()
        {

        }

        public void Stmt_List()
        {

        }

        public void Stmt()
        {

        }

        public void Assign_Stmt()
        {

        }

        public void If_Stmt()
        {

        }

        // if-stmt'
        public void If_Stmt2()
        {

        }

        public void Condition()
        {

        }

        public void While_Stmt()
        {

        }

        public void Stmt_Prefix()
        {

        }

        public void Read_Stmt()
        {

        }

        public void Write_Stmt()
        {

        }

        public void Writable()
        {

        }

        public void Expression()
        {

        }

        // expression'
        public void Expression2()
        {

        }

        public void Simple_Expr()
        {

        }

        public void Simple_Expr2()
        {

        }

        public void Term()
        {

        }

        public void Term2()
        {

        }

        public void Factor_A()
        {

        }

        public void Factor()
        {

        }

        public void RelOp()
        {

        }

        public void AddOp()
        {

        }

        public void MulOp()
        {

        }

        public void Constant()
        {

        }
    }
}
