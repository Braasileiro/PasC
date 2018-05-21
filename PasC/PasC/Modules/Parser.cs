using System;

using PasC.Models;

namespace PasC.Modules
{
	class Parser
	{
		// Lexer Resources
		private static Token TOKEN;




		// Parser Control
		public static void Set()
		{
			Advance();

			if (TOKEN.Lexeme == "program")
			{
				Eat(Tag.KW);

				if (!Eat(Tag.ID))
				{
					SyntacticError(String.Format("Expected \"<ID>\" but received \"{0}\".", TOKEN.Lexeme));
				}
			}
			else
			{
				SyntacticError(String.Format("Expected \"program\" but received \"{0}\".", TOKEN.Lexeme));

				Environment.Exit(1);
			}
		}




		// Errors
		private static void SyntacticError(String message)
		{
			Console.WriteLine("\n[SYNTACTIC ERROR]: " + message);
		}




		// Token Handlers
		private static void Advance()
		{
			TOKEN = Lexer.NextToken();

			Console.WriteLine("[DEBUG]" + TOKEN.ToString());
		}

		private static bool Eat(Tag tag)
		{
			if (TOKEN.GetTag() == tag)
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
		private static void Prog()
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
