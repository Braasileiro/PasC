using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State1
	{
		public static void Run()
		{
			string currentChar = Lexer.Read();

			// ->> 1
			if (DIGIT.IsMatch(currentChar))
			{
				Run();
			}
			
			// -> (2)
			State2.Run();

			// -> 3
			if (currentChar.Equals("."))
			{
				State3.Run();
			}
		}
	}
}
