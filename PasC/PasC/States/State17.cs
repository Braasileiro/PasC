using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State17
	{
		public static void Run()
		{
			string currentChar = Source.Get();

			// -> (18)
			if (currentChar.Equals("="))
			{
				State18.Run();
			}

			// -> (19)
			State19.Run();
		}
	}
}
