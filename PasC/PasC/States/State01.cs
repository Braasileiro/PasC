using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State01
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 1
			if (Char.IsDigit(CURRENT_CHAR))
			{
				State01.Run();
			}

			// -> 3
			if (CURRENT_CHAR == '.')
			{
				State03.Run();
			}

			// -> (2)
			State02.Run();
		}
	}
}
