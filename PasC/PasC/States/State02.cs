using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;
using PasC.Models;

namespace PasC.States
{
	class State02
	{
		public static void Run()
		{
            // FINAL STATE!

            // Adiciona token na tabela de simbolos
            Add(new Token(Tag.CON_NUM, "num_const", ROWS, COLUMNS), new Identifier());

            // Volta um caractere
            Lexer.Fallback();
		}
	}
}
