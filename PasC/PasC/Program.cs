using System;
using System.IO;
using PasC.Modules;

namespace PasC
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.Clear();

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
				Console.WriteLine("Uso: pasc.exe [caminho/arquivo_fonte.pc] (sem os colchetes)\n");

				Console.WriteLine("Pressione qualquer tecla para fechar...");
				Console.ReadKey();
				Environment.Exit(-1);
			}
			catch (FileNotFoundException)
			{
				ConsoleHeader();
				Console.WriteLine("[Error]: Invalid source file.");
				Environment.Exit(-1);
			}
			catch (Exception e)
			{
				ConsoleHeader();
				Console.WriteLine("[Error]: Fatal unhandled exception: {0}", e);
				Environment.Exit(-1);
			}

			ConsoleHeader();

			Global.SetEnvironment(args[0]);
        }

		private static void ConsoleHeader()
		{
			Console.WriteLine("\npasc (Framework 4.7) 2018.1.1 ALPHA");
			Console.WriteLine("Copyright (C) 2018 Lucas Cota, Carlos Alberto.\n");
		}
	}
}
