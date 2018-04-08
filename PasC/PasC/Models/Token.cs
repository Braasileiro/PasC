using System;

namespace PasC.Models
{
	class Token
	{
		public int Row { get; set; }
		public Tag TTag { get; set; }
		public int Column { get; set; }
		public String Lexeme { get; set; }

		public Token(Tag TTag, String Lexeme, int Row, int Column)
		{
			this.Row = Row;
			this.TTag = TTag;
			this.Column = Column;
			this.Lexeme = Lexeme;
		}

		public override string ToString()
		{
			return String.Format("<{0},\"{1}\">", TTag, Lexeme);
		}
	}
}
