using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State1
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 1
			if (DIGIT.IsMatch(CURRENT_CHAR))
			{
				Run();
			}
			
			// -> (2)
			State2.Run();

			// -> 3
			if (CURRENT_CHAR.Equals("."))
			{
				State3.Run();
			}
		}
	}
}
