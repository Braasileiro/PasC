using System;

namespace PasC.Modules
{
	class Global
	{
		public static char NewLine;

		// Set Enviroment Specific
		public static void SetEnvironment()
		{
			// NewLine
			switch (Environment.OSVersion.Platform)
			{
				// Windows, Unix
				case PlatformID.Win32NT: case PlatformID.Win32S: case PlatformID.Win32Windows: case PlatformID.WinCE: case PlatformID.Unix:
				{
					NewLine = '\n';
				} break;

				// Mac OSX
				case PlatformID.MacOSX:
				{
					NewLine = '\r';
				} break;
			}
		}
	}
}
