using System;
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
		private static Dictionary<Token, Identifier> SYMBOL_TABLE = new Dictionary<Token, Identifier>()
		{
			{ new Token(Tag.KW, "program", 0, 0), new Identifier() },
			{ new Token(Tag.KW, "if",      0, 0), new Identifier() },
			{ new Token(Tag.KW, "else",    0, 0), new Identifier() },
			{ new Token(Tag.KW, "while",   0, 0), new Identifier() },
			{ new Token(Tag.KW, "write",   0, 0), new Identifier() },
			{ new Token(Tag.KW, "read",    0, 0), new Identifier() },
			{ new Token(Tag.KW, "num",     0, 0), new Identifier() },
			{ new Token(Tag.KW, "char",    0, 0), new Identifier() },
			{ new Token(Tag.KW, "not",     0, 0), new Identifier() },
			{ new Token(Tag.KW, "or",      0, 0), new Identifier() },
			{ new Token(Tag.KW, "and",     0, 0), new Identifier() }

			// Dynamic Runtime Symbols...
		};




		// Getters and Setters
		public static void Add(Token key, Identifier value)
		{
			SYMBOL_TABLE.Add(key, value);
		}

		public static Identifier GetID(Token key)
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
