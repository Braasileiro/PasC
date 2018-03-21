using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State01
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 1
			if (DIGIT.IsMatch(CURRENT_CHAR))
			{
				State01.Run();
			}
			
			// -> (2)
			State02.Run();

			// -> 3
			if (CURRENT_CHAR.Equals("."))
			{
				State03.Run();
			}
		}
	}
}
