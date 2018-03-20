using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PasC.Models
{
	class Grammar
	{
		// Regex
		public static Regex DIGIT     = new Regex("[0-9]");
		public static Regex LETTER    = new Regex("[a-zA-Z]");
		public static Regex NUMCONST  = new Regex("[0-9]+(.[0-9]+)?");
		public static Regex CHARCONST = new Regex("['][\x00-\x7F][']");
		public static Regex LITERAL   = new Regex("[\"][\x00-\x7F]*[\"]");
		public static Regex ID        = new Regex("[a-zA-Z]+[a-zA-Z 0-9]*");




		// Symbol Table
		private static Dictionary<string, string> symbolTable = new Dictionary<string, string>()
		{
			// Keyword
			{ "program", "KW" },
			{ "if", "KW" },
			{ "else", "KW" },
			{ "while", "KW" },
			{ "read", "KW" },
			{ "write", "KW" },
			{ "num", "KW" },
			{ "char", "KW" },
			{ "and", "KW" },
			{ "or", "KW" },
			{ "not", "KW" },

			// Operator
			{ ">", "OP_GT" },
			{ "<", "OP_LT" },
			{ "+", "OP_AD" },
			{ "-", "OP_MIN" },
			{ "*", "OP_MUL" },
			{ "/", "OP_DIV" },
			{ "=", "OP_ASS" },
			{ "==", "OP_EQ" },
			{ "!=", "OP_NE" },
			{ ">=", "OP_GE" },
			{ "<=", "OP_LE" },

			// Delimiters
			{ "{", "SMB_OBC" },
			{ "}", "SMB_CBC" },
			{ "(", "SMB_OPA" },
			{ ")", "SMB_CPA" },
			{ ",", "SMB_COM" },
			{ ";", "SMB_SEM" },

			/*
			 * Dynamic Runtime Symbols
			 * ID: ID
			 * LIT: LITERAL
			 * CON_NUM: NUMCONST
			 * CON_CHAR: CHARCONST
			 */

			// ...
		};




		// Getters and Setters
		public static void Add(string key, string value)
		{
			symbolTable.Add(key, value);
		}

		public static string Get(string key)
		{
			return symbolTable[key];
		}
	}
}
