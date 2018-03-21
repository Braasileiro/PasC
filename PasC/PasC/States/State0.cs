using System;
using static PasC.Models.Grammar;

namespace PasC.States
{
	class State0
	{
		public static void Run()
		{
			string currentChar = Lexer.Read();

			// -> 1
			if (DIGIT.IsMatch(currentChar))
			{
				State1.Run();
			}

			// -> 6
			if (currentChar.Equals("'"))
			{
				State6.Run();
			}

			// -> 9
			if (currentChar.Equals("\""))
			{
				State9.Run();
			}

			// -> 12
			if (LETTER.IsMatch(currentChar))
			{
				// State12.Run();
			}

			// -> 14
			if (currentChar.Equals("="))
			{
				// State14.Run();
			}

			// -> 17
			if (currentChar.Equals(">"))
			{
				// State17.Run();
			}

			// -> 20
			if (currentChar.Equals("<"))
			{
				// State20.Run();
			}

			// -> 23
			if (currentChar.Equals("!"))
			{
				// State23.Run();
			}

			// -> (32)
			if (currentChar.Equals("*"))
			{
				// State32.Run();
			}

			// -> (33)
			if (currentChar.Equals("+"))
			{
				// State33.Run();
			}

			// -> (34)
			if (currentChar.Equals("-"))
			{
				// State34.Run();
			}

			// -> (35)
			if (currentChar.Equals("{"))
			{
				// State35.Run();
			}

			// -> (36)
			if (currentChar.Equals("}"))
			{
				// State36.Run();
			}

			// -> (37)
			if (currentChar.Equals("("))
			{
				// State37.Run();
			}

			// -> (38)
			if (currentChar.Equals(")"))
			{
				// State38.Run();
			}

			// -> (39)
			if (currentChar.Equals(","))
			{
				// State39.Run();
			}

			// -> (40)
			if (currentChar.Equals(";"))
			{
				// State40.Run();
			}
		}
	}
}
