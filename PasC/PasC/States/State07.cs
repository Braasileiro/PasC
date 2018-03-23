using static PasC.States.Lexer;

namespace PasC.States
{
	class State7
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (8)
			if (CURRENT_CHAR == '\'')
			{
				State08.Run();
			}
		}
	}
}
