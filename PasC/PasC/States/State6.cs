using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State6
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 7
			if (LETTER.IsMatch(CURRENT_CHAR) || DIGIT.IsMatch(CURRENT_CHAR))
			{
				State7.Run();
			}
		}
	}
}
