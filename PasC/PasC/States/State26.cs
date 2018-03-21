using System;
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
			if (LETTER.IsMatch(CURRENT_CHAR) || DIGIT.IsMatch(CURRENT_CHAR))
			{
				State26.Run();
			}

			// -> (27)
			if (CURRENT_CHAR.Equals("\n"))
			{
				State27.Run();
			}
		}
	}
}
