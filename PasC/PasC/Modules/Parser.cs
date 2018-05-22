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
		private static void SyntacticError(String message)
		{
			ERROR_COUNT++;

			Console.WriteLine("\n[SYNTACTIC ERROR]: " + message);

			if (ERROR_COUNT == 5)
			{
				Console.WriteLine("\n[SYNTACTIC ERROR]: Many syntactic errors, aborting execution.");

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
					SyntacticError(String.Format("Expected \"<ID>\" but received \"{0}\".", TOKEN.Lexeme));
				}

				Body();
			}
			else
			{
				SyntacticError(String.Format("Expected \"program\" but received \"{0}\".", TOKEN.Lexeme));
			}
		}

		//body -> decl-list "{" stmt-list "}"
		private static void Body()
		{
			if (GetTag() == Tag.KW_NUM || GetTag() == Tag.KW_CHAR)
			{
				Decl_List();
			}

			else if (Eat(Tag.SMB_OBC))
			{
				Stmt_List();

				if (!Eat(Tag.SMB_CBC))
				{
					SyntacticError(String.Format("Expected \"}}\" but received \"{0}\".", TOKEN.Lexeme));
				}
			}
		}

		private static void Decl_List()
		{
			
		}

		private static void Decl()
		{

		}

		private static void Type()
		{

		}

		private static void Id_List()
		{

		}

		// id-list'
		private static void Id_List2()
		{

		}

		private static void Stmt_List()
		{

		}

		private static void Stmt()
		{

		}

		private static void Assign_Stmt()
		{

		}

		private static void If_Stmt()
		{

		}

		// if-stmt'
		private static void If_Stmt2()
		{

		}

		private static void Condition()
		{

		}

		private static void While_Stmt()
		{

		}

		private static void Stmt_Prefix()
		{

		}

		private static void Read_Stmt()
		{

		}

		private static void Write_Stmt()
		{

		}

		private static void Writable()
		{

		}

		private static void Expression()
		{

		}

		// expression'
		private static void Expression2()
		{

		}

		private static void Simple_Expr()
		{

		}

		private static void Simple_Expr2()
		{

		}

		private static void Term()
		{

		}

		private static void Term2()
		{

		}

		private static void Factor_A()
		{

		}

		private static void Factor()
		{

		}

		private static void RelOp()
		{

		}

		private static void AddOp()
		{

		}

		private static void MulOp()
		{

		}

		private static void Constant()
		{

		}
	}
}
