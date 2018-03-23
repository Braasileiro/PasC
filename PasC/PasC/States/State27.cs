using static PasC.States.Lexer;

namespace PasC.States
{
	class State27
	{
		public static void Run()
		{
			Lexer.Read();

			// ->> 27
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State27.Run();
			}

			// -> 28
			if (CURRENT_CHAR == '*')
			{
				State28.Run();
			}
		}
	}
}
