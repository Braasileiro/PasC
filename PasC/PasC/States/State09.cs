using static PasC.States.Lexer;

namespace PasC.States
{
	class State09
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 10
			if (Lexer.IsASCII(CURRENT_CHAR))
			{
				State10.Run();
			}
		}
	}
}
