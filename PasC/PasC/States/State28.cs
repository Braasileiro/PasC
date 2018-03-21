using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State28
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 29
			if (CURRENT_CHAR.Equals("*"))
			{
				State29.Run();
			}
		}
	}
}
