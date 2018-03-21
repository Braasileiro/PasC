using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State12
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 12
			if (LETTER.IsMatch(CURRENT_CHAR) || DIGIT.IsMatch(CURRENT_CHAR))
			{
				Run();
			}

			// -> (13)
			State13.Run();
		}
	}
}
