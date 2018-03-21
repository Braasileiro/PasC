using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State23
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (24)
			if (CURRENT_CHAR.Equals("="))
			{
				State24.Run();
			}
		}
	}
}
