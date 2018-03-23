using PasC.Models;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State26
	{
		public static void Run()
		{
			// TODO: FINAL STATE!

			Lexer.Read();

			// ->> 26
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State26.Run();
			}
		}
	}
}
