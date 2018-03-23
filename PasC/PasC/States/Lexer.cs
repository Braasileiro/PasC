using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PasC.States
{
	class Lexer
	{
		// Control
		public static int ROW;
		public static int COLUMN;
		public static int LAST_CHAR = 0;
		public static char CURRENT_CHAR;
		public static StringBuilder LEXEME;
		public static readonly int EOF = -1;


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
			LEXEME = new StringBuilder();

			try
			{
				LAST_CHAR = sourceFile.ReadByte();

				if (LAST_CHAR != EOF)
				{
					CURRENT_CHAR = (char) LAST_CHAR;
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

		public static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), "[\x00-\xFF]");
		}
	}
}
