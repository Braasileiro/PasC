using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State7
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// -> (8)
			if (currentChar.Equals("'"))
			{
				State8.Run();
			}
		}
	}
}
