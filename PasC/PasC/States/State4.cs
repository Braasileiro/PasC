using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State4
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// ->> 4
			if (DIGIT.IsMatch(currentChar))
			{
				Run();
			}

			// -> (5)
			State5.Run();
		}
	}
}
