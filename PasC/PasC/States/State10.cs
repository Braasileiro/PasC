using static PasC.States.Lexer;

namespace PasC.States
{
	class State10
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (11)
			if (CURRENT_CHAR == '\"')
			{
				State11.Run();
			}

			// ->> 10
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State10.Run();
			}
		}
	}
}
