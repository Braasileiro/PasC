using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State12
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 12
			if (Char.IsLetterOrDigit(CURRENT_CHAR))
			{
				State12.Run();
			}

			// -> (13)
			State13.Run();
		}
	}
}
