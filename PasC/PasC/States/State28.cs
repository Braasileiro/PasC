using static PasC.States.Lexer;

namespace PasC.States
{
	class State28
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 28
			if (CURRENT_CHAR == '*')
			{
				State28.Run();
			}

			// -> (29)
			if (CURRENT_CHAR == '/')
			{
				State29.Run();
			}

			// <- 27
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State27.Run();
			}
		}
	}
}
