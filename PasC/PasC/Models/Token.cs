using System;

using PasC.Modules;

namespace PasC.Models
{
	class Token
	{
		public Tag Type { get; set; }
		public int NoType { get; set; }
		public String Lexeme { get; set; }

		public Token(Tag Type, String Lexeme, int Row, int Column)
		{
			this.Row = Row;
			this.Type = Type;
			this.Column = Column;
			this.Lexeme = Lexeme;
            this.NoType = No.TYPE_EMPTY;
		}

		private int row; public int Row
		{
			get
			{
				if (nameof(Type).Contains("KW"))
				{
					return row;
				}
				else
				{
					return Lexer.ROW;
				}
			}
			set
			{
				row = value;
			}
		}

		private int column; public int Column
		{
			get
			{
				if (nameof(Type).Contains("KW"))
				{
					return column;
				}
				else
				{
					return Lexer.COLUMN;
				}
			}
			set
			{
				column = value;
			}
		}

		public Tag GetTag()
        {
            return Type;
        }

		public override string ToString()
		{
			return String.Format("<{0},\"{1}\">, Type: {2}", Type, Lexeme, Tipo);
		}
    }
}
