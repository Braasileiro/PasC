using static PasC.States.Lexer;

namespace PasC.States
{
	class State25
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 26
			if (CURRENT_CHAR == '/')
			{
				State26.Run();
			}

			// -> 27
			if (CURRENT_CHAR == '*')
			{
				State27.Run();
			}

			// -> (30)
			State30.Run();
		}
	}
}
