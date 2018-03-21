using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State12
	{
		public static void Run()
		{
			string currentChar = Lexer.Read();

			// ->> 12
			if (LETTER.IsMatch(currentChar) || DIGIT.IsMatch(currentChar))
			{
				Run();
			}

			// -> (13)
			State13.Run();
		}
	}
}
