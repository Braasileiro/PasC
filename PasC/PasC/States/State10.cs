using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State10
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// ->> 10
			if (LETTER.IsMatch(currentChar) || DIGIT.IsMatch(currentChar))
			{
				Run();
			}

			// -> (11)
			if (currentChar.Equals("\""))
			{
				State11.Run();
			}
		}
	}
}
