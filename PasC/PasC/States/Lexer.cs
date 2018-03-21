using System;
using System.IO;

namespace PasC.States
{
	class Lexer
	{
		// Control
		public static int ROWS;
		public static int COLUMNs;
		public static int LAST_CHAR = 0;
		public static readonly int EOF = -1;


		// Source
		private static FileStream sourceFile;




		public static void Set(string source)
		{
			sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);
		}

		public static char Read()
		{
			char CURRENT_CHAR = '\u0000';

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

			return CURRENT_CHAR;
		}

		public static void Fallback()
		{
			try
			{
				if (LAST_CHAR != EOF)
				{
					sourceFile.Seek(sourceFile.Position - 1, SeekOrigin.Current);
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the source file.\n{0}", e);
				Environment.Exit(2);
			}
		}
	}
}
