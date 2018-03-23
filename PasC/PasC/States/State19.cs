﻿using PasC.Models;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State19
	{
		public static void Run()
		{
			// FINAL STATE!
			IsAFinalState();

			// Adiciona token na tabela de símbolos
			Add(new Token(Tag.OP_GT, GetLexeme(), ROW, COLUMN), new Identifier());

			// Volta um caractere
			Lexer.Fallback();
		}
	}
}
