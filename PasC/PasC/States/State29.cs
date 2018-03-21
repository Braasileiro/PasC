using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State29
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (30)
			if (CURRENT_CHAR.Equals("/"))
			{
				State30.Run();
			}
		}
	}
}
