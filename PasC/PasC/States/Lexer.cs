using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PasC.States
{
	class Lexer
	{
		// Source Pointers
		public static int ROW;
		public static int COLUMN;
		public static char CURRENT_CHAR;
		private static StringBuilder LEXEME;

		// File Pointers
		public static int LAST_CHAR = 0;
		public static readonly int EOF = -1;

		// Check
		public static bool FINAL_STATE;

		// Source
		private static FileStream sourceFile;




		public static void Set(string source)
		{
			sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);

			State00.Run();
		}

		public static void Read()
		{
			CURRENT_CHAR = '\u0000';

			if (FINAL_STATE)
			{
				FINAL_STATE = false;
				     LEXEME = new StringBuilder();
			}

			try
			{
				LAST_CHAR = sourceFile.ReadByte();

				if (LAST_CHAR != EOF)
				{
					CURRENT_CHAR = (char) LAST_CHAR;
					LEXEME.Append(CURRENT_CHAR);
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the character '{0}'\n{1}", CURRENT_CHAR, e);
				Environment.Exit(1);
			}
		}

		public static void Fallback()
		{
			try
			{
				if (LAST_CHAR != EOF)
				{
					sourceFile.Seek(sourceFile.Position - 1, SeekOrigin.Current);

                    State00.Run(); // Reinicia o autômato
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the source file.\n{0}", e);
				Environment.Exit(2);
			}
		}

		public static string GetLexeme()
		{
			return LEXEME.ToString();
		}

		public static void IsAFinalState()
		{
			FINAL_STATE = true;
		}

		public static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), "[\x00-\xFF]");
		}
	}
}
