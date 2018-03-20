using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State6
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// -> 7
			if (LETTER.IsMatch(currentChar) || DIGIT.IsMatch(currentChar))
			{
				// State7.Run();
			}
		}
	}
}
