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
			}

			if (!Eat(Tag.OP_ASS))
			{
				SyntacticError("\"=\"");
			}

			Simple_Expr();
		}


		// if-stmt -> "if" "(" condition ")" "{" stmt-list "}" if-stmt'
		private static void If_Stmt()
		{
			if (!Eat(Tag.KW_IF))
			{
				SyntacticError("\"if\"");
			}

			if (!Eat(Tag.SMB_OPA))
			{
				SyntacticError("\"(\"");
			}

			Condition();

			if (!Eat(Tag.SMB_CPA))
			{
				SyntacticError("\")\"");
			}

			if (!Eat(Tag.SMB_OBC))
			{
				SyntacticError("\"{\"");
			}

			Stmt_List();

			if (!Eat(Tag.SMB_CBC))
			{
				SyntacticError("\"}\"");
			}

			If_Stmt2();
		}


		// if-stmt' -> "else" "{" stmt-list "}" | ε
		private static void If_Stmt2()
		{
			// ε -> ";"
			if (Eat(Tag.SMB_SEM))
			{
				return;
			}

			// "else" "{" stmt-list "}"
			else
			{
				if (!Eat(Tag.KW_ELSE))
				{
					SyntacticError("\"else\"");
				}

				if (!Eat(Tag.SMB_OBC))
				{
					SyntacticError("\"{\"");
				}

				Stmt_List();

				if (!Eat(Tag.SMB_CBC))
				{
					SyntacticError("\"}\"");
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
			}

			Stmt_List();

			if (!Eat(Tag.SMB_CBC))
			{
				SyntacticError("\"}\"");
			}
		}


		// stmt-prefix -> "while" "(" condition ")"
		private static void Stmt_Prefix()
		{
			if (!Eat(Tag.KW_WHILE))
			{
				SyntacticError("\"while\"");
			}

			if (!Eat(Tag.SMB_OPA))
			{
				SyntacticError("\"(\"");
			}

			Condition();

			if (!Eat(Tag.SMB_CPA))
			{
				SyntacticError("\")\"");
			}
		}


		// read-stmt -> "read" "id"
		private static void Read_Stmt()
		{
			if (!Eat(Tag.KW_READ))
			{
				SyntacticError("\"read\"");
			}

			if (!Eat(Tag.ID))
			{
				SyntacticError("\"<ID>\"");
			}
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
