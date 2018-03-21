using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State25
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 26
			if (CURRENT_CHAR.Equals("/"))
			{
				State26.Run();
			}

			// -> 28
			if (CURRENT_CHAR.Equals("*"))
			{
				State28.Run();
			}

			// -> (31)
			State31.Run();
		}
	}
}
