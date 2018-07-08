using System;
using System.Linq;
using System.Collections.Generic;

namespace PasC.Models
{
	class No
	{
		private static int SPACE = 0;
		public int TYPE = TYPE_EMPTY;
		private List<No> Childs { get; }
		private Token Father { get; set; }

		// Constantes para tipos
		public static int TYPE_BOOL = 100;
		public static int TYPE_NUM = 101;
		public static int TYPE_CHAR = 103;
		public static int TYPE_ERRO = 104;
		public static int TYPE_EMPTY = 111;
		public static int TYPE_LITERAL = 102;


		public No(Token token)
		{
			this.Father = token;
			this.Childs = new List<No>();
		}

		public void AddAll(List<No> childList)
		{
			this.Childs.Union(childList);
		}

		public void AddChild(No child)
		{
			this.Childs.Add(child);
		}

		public void PrintContent()
		{
			if (Father != null)
			{
				for (int i = 0; i < SPACE; i++)
				{
					Console.WriteLine(".   ");
				}

				Console.WriteLine(Father.ToString() + " - Tipo: " + TYPE + "\n");

				SPACE++;
			}

			foreach (No child in Childs)
			{
				child.PrintContent();
			}

			if (Father != null)
			{
				SPACE--;
			}
		}
	}
}
