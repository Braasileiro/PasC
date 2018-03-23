using PasC.Models;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State26
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 26
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State26.Run();
			}

			// FINAL STATE!
			IsAFinalState();

			// Adiciona token na tabela de símbolos
			Add(new Token(Tag.COM_ONL, GetLexeme(), ROW, COLUMN), new Identifier());

			// Volta um caractere
			Lexer.Fallback();
		}
	}
}
