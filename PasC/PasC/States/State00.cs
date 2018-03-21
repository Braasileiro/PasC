using System;
using static PasC.States.Lexer;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State00
	{
		public static void Run()
		{
			Lexer.Read();

			// -> 1
			if (DIGIT.IsMatch(CURRENT_CHAR))
			{
				State01.Run();
			}

			// -> 6
			if (CURRENT_CHAR.Equals("'"))
			{
				State06.Run();
			}

			// -> 9
			if (CURRENT_CHAR.Equals("\""))
			{
				State09.Run();
			}

			// -> 12
			if (LETTER.IsMatch(CURRENT_CHAR))
			{
				State12.Run();
			}

			// -> 14
			if (CURRENT_CHAR.Equals("="))
			{
				State14.Run();
			}

			// -> 17
			if (CURRENT_CHAR.Equals(">"))
			{
				State17.Run();
			}

			// -> 20
			if (CURRENT_CHAR.Equals("<"))
			{
				State20.Run();
			}

			// -> 23
			if (CURRENT_CHAR.Equals("!"))
			{
				State23.Run();
			}

			// -> 25
			if (CURRENT_CHAR.Equals("/"))
			{
				State25.Run();
			}

			// -> (32)
			if (CURRENT_CHAR.Equals("*"))
			{
				State32.Run();
			}

			// -> (33)
			if (CURRENT_CHAR.Equals("+"))
			{
				State33.Run();
			}

			// -> (34)
			if (CURRENT_CHAR.Equals("-"))
			{
				State34.Run();
			}

			// -> (35)
			if (CURRENT_CHAR.Equals("{"))
			{
				State35.Run();
			}

			// -> (36)
			if (CURRENT_CHAR.Equals("}"))
			{
				State36.Run();
			}

			// -> (37)
			if (CURRENT_CHAR.Equals("("))
			{
				State37.Run();
			}

			// -> (38)
			if (CURRENT_CHAR.Equals(")"))
			{
				State38.Run();
			}

			// -> (39)
			if (CURRENT_CHAR.Equals(","))
			{
				State39.Run();
			}

			// -> (40)
			if (CURRENT_CHAR.Equals(";"))
			{
				State40.Run();
			}
		}
	}
}
