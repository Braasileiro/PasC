using System;
using System.IO;

namespace PasC
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length < 1)
				{
					throw new IndexOutOfRangeException();
				}

				if (!File.Exists(args[0]))
				{
					throw new FileNotFoundException();
				}
			}
			catch (IndexOutOfRangeException)
			{
				ConsoleHeader();
				Console.WriteLine("Usage: pasc [source.pasc]");
			}
			catch (FileNotFoundException)
			{
				ConsoleHeader();
				Console.WriteLine("[Error]: Invalid source file.");
			}
			catch (Exception e)
			{
				ConsoleHeader();
				Console.WriteLine("[Error]: Fatal unhandled exception: {0}", e);
			}
		}

		private static void ConsoleHeader()
		{
			Console.WriteLine("pasc (Framework 4.7) 2018.1.1 ALPHA");
			Console.WriteLine("Copyright (C) 2018 Lucas Cota, Carlos Alberto.\n");
		}
	}
}
