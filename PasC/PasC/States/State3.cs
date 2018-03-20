using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State3
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// -> 4
			if (DIGIT.IsMatch(currentChar))
			{
				State4.Run();
			}
		}
	}
}
