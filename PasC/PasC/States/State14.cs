using static PasC.States.Lexer;

namespace PasC.States
{
	class State14
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (15)
			if (CURRENT_CHAR == '=')
			{
				State15.Run();
			}

			// -> (16)
			State16.Run();
		}
	}
}
