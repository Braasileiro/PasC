using static PasC.States.Lexer;

namespace PasC.States
{
	class State17
	{
		public static void Run()
		{
			Lexer.Read();

			// -> (18)
			if (CURRENT_CHAR == '=')
			{
				State18.Run();
			}

			// -> (19)
			State19.Run();
		}
	}
}
