using PasC.Models;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State02
	{
		public static void Run()
		{
			// FINAL STATE!
			IsAFinalState();

            // Adiciona token na tabela de símbolos
            Add(new Token(Tag.CON_NUM, GetLexeme(), ROW, COLUMN), new Identifier());

            // Volta um caractere
            Lexer.Fallback();
		}
	}
}
