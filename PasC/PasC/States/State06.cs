using static PasC.States.Lexer;

namespace PasC.States
{
	class State06
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 7
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State7.Run();
			}
		}
	}
}
