using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasC.Models
{
    class No
    {
        private Token father { get; set; }
        private List<No> childList { get; }
        private static int space = 0;

        // Constantes para tipos
        public static int TYPE_EMPTY = 111;
        public static int TYPE_BOOL = 100;
        public static int TYPE_NUM = 101;
        public static int TYPE_LITERAL = 102;
        public static int TYPE_CHAR = 103;
        public static int TYPE_ERRO = 104;

        public int type = TYPE_EMPTY;

        public No(Token token)
        {
            this.father = token;
            this.childList = new List<No>();
        }
        
        public void AddAll(List<No> childList)
        {
            this.childList.Union(childList);
        }

        public void AddChild(No child)
        {
            this.childList.Add(child);
        }

        public void PrintContent()
        {
            if (this.father != null)
            {
                for (int i = 0; i < space; i++)
                {
                    Console.WriteLine(".   ");
                }
                Console.WriteLine(this.father.ToString() + " - Tipo: " + this.type + "\n");
                space++;
            }
            foreach (No child in childList)
            {
                child.PrintContent();
            }
            if (this.father != null)
            {
                space--;
            }
        }
    }
}
