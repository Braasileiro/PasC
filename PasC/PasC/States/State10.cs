using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State10
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 10
			if (LETTER.IsMatch(CURRENT_CHAR) || DIGIT.IsMatch(CURRENT_CHAR))
			{
				State10.Run();
			}

			// -> (11)
			if (CURRENT_CHAR.Equals("\""))
			{
				State11.Run();
			}
		}
	}
}
