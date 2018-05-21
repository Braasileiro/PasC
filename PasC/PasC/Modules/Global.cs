using System;
using System.IO;

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


			// Source File
			SOURCE = new FileStream(sourceFile, FileMode.Open, FileAccess.ReadWrite);
		}
	}
}
