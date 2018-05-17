using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PasC.Modules;

namespace PasC.Models
{
    class Parser
    {
        private Lexer lexer;
        private Token token;

        public Parser(Lexer lexer)
        {

        }

        public void SyntacticError(String message)
        {
            Console.WriteLine("\n[SYNTACTIC ERROR]: " + message);
        }

        public void Advance()
        {
            //token = lexer.getToken();
            Console.WriteLine("[DEBUG]" + token.ToString());
        }

        // Verificacao de token esperado
        public bool Eat(Tag tag)
        {
            if(token.GetTag() == tag)
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
        /// <summary>
        /// Todas as decisoes sao baseadas na tabela preditiva
        /// </summary>
        public void Prog()
        {

        }

        public void Body()
        {

        }

        public void Decl_list()
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

        public void Relop()
        {

        }

        public void Addop()
        {

        }

        public void Mulop()
        {

        }

        public void Constant()
        {

        }



    }
}
