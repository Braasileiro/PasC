using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State14
	{
		public static void Run()
		{
			string currentChar = Lexer.Read();

			// -> (15)
			if (currentChar.Equals("="))
			{
				State15.Run();
			}

			// -> (16)
			State16.Run();
		}
	}
}
