using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State03
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 4
			if (Char.IsDigit(CURRENT_CHAR))
			{
				State04.Run();
			}
		}
	}
}
