using static PasC.States.Lexer;

namespace PasC.States
{
	class State20
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (21)
			if (CURRENT_CHAR == '=')
			{
				State21.Run();
			}

			// -> (22)
			State22.Run();
		}
	}
}
