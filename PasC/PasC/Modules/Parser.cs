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

			Console.WriteLine("\n[SYNTACTIC ERROR]: Expected {0} but received \"{1}\".", lexeme, TOKEN.Lexeme);

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
					SyntacticError("\"<ID>\"");
				}

				Body();
			}
			else
			{
				SyntacticError("\"program\"");
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
				}
			}
		}


		// decl-list -> decl ";" decl-list | ε
		private static void Decl_List()
		{
			// ε -> "{"
			if (Eat(Tag.SMB_OBC))
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
			}
		}


		// id-list -> "id" id-list'
		private static void Id_List()
		{
			if (!Eat(Tag.ID))
			{
				SyntacticError("\"<ID>\"");
			}

			Id_List2();
		}


		// id-list' -> "," id-list | ε
		private static void Id_List2()
		{
			// ε -> ";"
			if (Eat(Tag.SMB_SEM))
			{
				return;
			}

			// "," id-list
			else
			{
				if (!Eat(Tag.SMB_COM))
				{
					SyntacticError("\",\"");
				}

				Id_List();
			}
		}


		// stmt-list -> stmt ";" stmt-list | ε
		private static void Stmt_List()
		{
			// ε -> "}"
			if (Eat(Tag.SMB_CBC))
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
				}

				Stmt_List();
			}
		}


		// stmt -> assign-stmt | if-stmt | while-stmt | read-stmt | write-stmt
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
