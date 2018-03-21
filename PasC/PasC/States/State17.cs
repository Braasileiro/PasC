using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State17
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (18)
			if (CURRENT_CHAR.Equals("="))
			{
				State18.Run();
			}

			// -> (19)
			State19.Run();
		}
	}
}
