using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PasC.Models
{
	class Grammar
	{
		// Regex
		public static Regex DIGIT     = new Regex(@"[0-9]");
		public static Regex LETTER    = new Regex(@"[a-zA-Z]");
		public static Regex NUMCONST  = new Regex(@"[0-9]+(.[0-9]+)?");
		public static Regex CHARCONST = new Regex(@"['][\x20-\xFF][']");
		public static Regex LITERAL   = new Regex("[\"][\x20-\xFF]*[\"]");
		public static Regex ID        = new Regex(@"[a-zA-Z]+[a-zA-Z 0-9]*");




		// Symbol Table
		private static Dictionary<Token, String> SYMBOL_TABLE = new Dictionary<Token, String>()
		{
			{ new Token(Tag.KW_PROGRAM, "program", 0, 0), "program" },
			{ new Token(Tag.KW_IF,      "if",      0, 0), "if" },
			{ new Token(Tag.KW_ELSE,    "else",    0, 0), "else" },
			{ new Token(Tag.KW_WHILE,   "while",   0, 0), "while" },
			{ new Token(Tag.KW_WRITE,   "write",   0, 0), "write" },
			{ new Token(Tag.KW_READ,    "read",    0, 0), "read" },
			{ new Token(Tag.KW_NUM,     "num",     0, 0), "num" },
			{ new Token(Tag.KW_CHAR,    "char",    0, 0), "char" },
			{ new Token(Tag.KW_NOT,     "not",     0, 0), "not" },
			{ new Token(Tag.KW_OR,      "or",      0, 0), "or" },
			{ new Token(Tag.KW_AND,     "and",     0, 0), "and" }

			// Dynamic Runtime Symbols...
		};

		public static void Show()
		{
			Console.WriteLine("\nPasC::Symbols\n");

			foreach (var currentSymbol in SYMBOL_TABLE)
			{
				Console.WriteLine("Symbol: [{0}]", currentSymbol.Key);
			}
		}




		// Getters and Setters
		public static void Add(Token key, String value)
		{
			SYMBOL_TABLE.Add(key, value);
		}

		public static Identifier GetID(String key)
		{
			return SYMBOL_TABLE[key];
		}

		public static Token GetToken(String lexeme)
		{
			foreach (Token currentToken in SYMBOL_TABLE.Keys)
			{
				if (currentToken.Lexeme.Equals(lexeme))
				{
					return currentToken;
				}
			}

			return null;
		}
	}
}
