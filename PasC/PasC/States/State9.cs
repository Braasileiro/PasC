using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State9
	{
		public static void Run()
		{
			string currentChar = Lexer.Read();

			// -> 10
			if (LETTER.IsMatch(currentChar) || DIGIT.IsMatch(currentChar))
			{
				State10.Run();
			}
		}
	}
}
