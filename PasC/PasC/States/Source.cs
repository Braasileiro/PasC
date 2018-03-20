using System;
using System.IO;

namespace PasC.States
{
	class Source
	{
		private static int sourceChar = 0;
		private static String[] sourceCode;

		public static void Set(string sourceFile)
		{
			sourceCode = File.ReadAllText(sourceFile).Split();
		}

		public static string Get()
		{
			string currentChar = sourceCode[sourceChar];

			if (currentChar.Equals("\t"))
			{
				currentChar = "   ";
			}

			Count();

			return currentChar;
		}

		private static void Count()
		{
			sourceChar++;
		}
	}
}
