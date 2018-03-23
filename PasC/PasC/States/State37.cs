using PasC.Models;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State37
	{
		public static void Run()
		{
			// FINAL STATE!
			IsAFinalState();

			// Adiciona token na tabela de símbolos
			Add(new Token(Tag.SMB_CPA, GetLexeme(), ROW, COLUMN), new Identifier());

			// Volta um caractere
			Lexer.Fallback();
		}
	}
}
