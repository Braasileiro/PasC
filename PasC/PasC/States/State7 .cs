using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State7
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (8)
			if (CURRENT_CHAR.Equals("'"))
			{
				State8.Run();
			}
		}
	}
}
