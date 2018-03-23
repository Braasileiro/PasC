using System;
using static PasC.States.Lexer;

namespace PasC.States
{
	class State00
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 1
			if (Char.IsDigit(CURRENT_CHAR))
			{
				State01.Run();
			}

			// -> 6
			if (CURRENT_CHAR == '\'')
			{
				State06.Run();
			}

			// -> 9
			if (CURRENT_CHAR == '\'')
			{
				State09.Run();
			}

			// -> 12
			if (Char.IsLetter(CURRENT_CHAR))
			{
				State12.Run();
			}

			// -> 14
			if (CURRENT_CHAR == '=')
			{
				State14.Run();
			}

			// -> 17
			if (CURRENT_CHAR == '>')
			{
				State17.Run();
			}

			// -> 20
			if (CURRENT_CHAR == '<')
			{
				State20.Run();
			}

			// -> 23
			if (CURRENT_CHAR == '!')
			{
				State23.Run();
			}

			// -> 25
			if (CURRENT_CHAR == '/')
			{
				State25.Run();
			}

			// -> (32)
			if (CURRENT_CHAR == '*')
			{
				State32.Run();
			}

			// -> (33)
			if (CURRENT_CHAR == '+')
			{
				State33.Run();
			}

			// -> (34)
			if (CURRENT_CHAR == '-')
			{
				State34.Run();
			}

			// -> (35)
			if (CURRENT_CHAR == '{')
			{
				State35.Run();
			}

			// -> (36)
			if (CURRENT_CHAR == '}')
			{
				State36.Run();
			}

			// -> (37)
			if (CURRENT_CHAR == '(')
			{
				State37.Run();
			}

			// -> (38)
			if (CURRENT_CHAR == ')')
			{
				State38.Run();
			}

			// -> (39)
			if (CURRENT_CHAR == ',')
			{
				State39.Run();
			}

			// -> (40)
			if (CURRENT_CHAR == ';')
			{
				State40.Run();
			}
		}
	}
}
