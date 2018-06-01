using PasC.Models;
using System;
using System.IO;
using System.Linq;

namespace PasC.Modules
{
	class Global
	{
		// NewLine Character
		public static char NEW_LINE;

		// Source File
		public static FileStream SOURCE;




		// Set Compiler Enviroment
		public static void SetEnvironment(string sourceFile)
		{
			// NewLine Character
			switch (Environment.OSVersion.Platform)
			{
				// Windows, Unix
				case PlatformID.Win32NT: case PlatformID.Win32S: case PlatformID.Win32Windows: case PlatformID.WinCE: case PlatformID.Unix:
				{
					NEW_LINE = '\n';
				} break;

				// Mac OSX
				case PlatformID.MacOSX:
				{
					NEW_LINE = '\r';
				} break;
			}

			/* 
			 * Adiciona uma nova linha no arquivo como forma de segurança
			 * caso a última linha do arquivo não seja uma linha em branco.
			 * Isso evita o último token/caracter do arquivo de não ser reconhecido.
			*/
			if (!String.IsNullOrWhiteSpace(File.ReadAllLines(sourceFile).Last()))
			{
				File.AppendAllText(sourceFile, Environment.NewLine);
			}

			// Source File
			SOURCE = new FileStream(sourceFile, FileMode.Open, FileAccess.ReadWrite);
		}

		public static void EndParsing()
		{
			SOURCE.Close();

			Grammar.Show();

			Console.WriteLine("\nPress ENTER to continue...");

			Console.ReadKey();

			Environment.Exit(0);
		}
	}
}
