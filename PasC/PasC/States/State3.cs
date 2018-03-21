using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State3
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 4
			if (DIGIT.IsMatch(CURRENT_CHAR))
			{
				State4.Run();
			}
		}
	}
}
