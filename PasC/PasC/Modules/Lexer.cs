using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using PasC.Models;

namespace PasC.Modules
{
	class Lexer
	{
		// Source Pointers
		public static int ROW = 1;
		public static int COLUMN = 1;
		public static Token LAST_TOKEN;
		public static char CURRENT_CHAR;
		private static StringBuilder LEXEME;

		// File Pointers
		public static int LAST_CHAR = 0;
		public static readonly int EOF = -1;

		// Check
		public static int STATE;

		// Source
		private static FileStream sourceFile;




		// Methods
		public static void Set(string source)
		{
			Token token;
			LEXEME = new StringBuilder();
			sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);

			do
			{
				LEXEME.Clear();
				token = NextToken();

				if (token != null)
				{
					Console.WriteLine("Token: " + token.ToString() + "\t Line: " + ROW + "\t Column: " + COLUMN);
				}

				LAST_TOKEN = token;

				if (Grammar.GetToken(GetLexeme()) == null && LAST_TOKEN != null)
				{
					Grammar.Add(LAST_TOKEN, new Identifier());
				}

			} while (!token.Lexeme.Equals(Tag.EOF) && token != null);

			sourceFile.Close();
		}

		public static void Read()
		{
			CURRENT_CHAR = '\u0000';

			try
			{
				LAST_CHAR = sourceFile.ReadByte();

				if (LAST_CHAR != EOF)
				{
					CURRENT_CHAR = (char)LAST_CHAR;
					COLUMN++;
				}
				else
				{
					Grammar.Add(new Token(Tag.EOF, GetLexeme(), ROW, COLUMN), new Identifier());
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the character '{0}'\n{1}", CURRENT_CHAR, e);
				Environment.Exit(1);
			}
		}

		public static void Restart()
		{
			STATE = 0;

			try
			{
				if (LAST_CHAR != EOF)
				{
					sourceFile.Seek(sourceFile.Position - 1, SeekOrigin.Begin);
					COLUMN--;

				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the source file.\n{0}", e);
				Environment.Exit(2);
			}
		}

		public static void LexicalError(String message)
		{
			Console.WriteLine("[Lexical Error]: " + message + "\n");
		}

		public static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), "[\x00-\xFF]");
		}
		



		public static Token NextToken()
		{
			STATE = 0;

			while (true)
			{
				Read();

				switch (STATE)
				{
					// State0
					case 0:
					{
						// ->> 0
						if (Char.IsWhiteSpace(CURRENT_CHAR))
						{
							STATE = 0;
						}

						// ->> 0
						else if (CURRENT_CHAR == '\n' || CURRENT_CHAR == '\r')
						{
							STATE = 0;
							CURRENT_CHAR = (char) sourceFile.ReadByte();
						}

						// -> 1
						else if (Char.IsDigit(CURRENT_CHAR))
						{
							STATE = 1;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 6
						else if (CURRENT_CHAR.Equals('\''))
						{
							STATE = 6;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 9
						else if (CURRENT_CHAR.Equals('\"'))
						{
							STATE = 9;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 12
						else if (Char.IsLetter(CURRENT_CHAR))
						{
							STATE = 12;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 14
						else if (CURRENT_CHAR.Equals('='))
						{
							STATE = 14;
						}

						// -> 17
						else if (CURRENT_CHAR.Equals('>'))
						{
							STATE = 17;
						}

						// -> 20
						else if (CURRENT_CHAR.Equals('<'))
						{
							STATE = 20;
						}

						// -> 23
						else if (CURRENT_CHAR.Equals('!'))
						{
							STATE = 23;
						}

						// -> 25
						else if (CURRENT_CHAR.Equals('/'))
						{
							STATE = 25;
						}

						// -> (31)
						else if (CURRENT_CHAR.Equals('*'))
						{
							STATE = 31;
						}

						// -> (32)
						else if (CURRENT_CHAR.Equals('+'))
						{
							STATE = 32;
						}

						// -> (33)
						else if (CURRENT_CHAR.Equals('-'))
						{
							STATE = 33;
						}

						// -> (34)
						else if (CURRENT_CHAR.Equals('{'))
						{
							STATE = 34;
						}

						// -> (35)
						else if (CURRENT_CHAR.Equals('}'))
						{
							STATE = 35;
						}

						// -> (36)
						else if (CURRENT_CHAR.Equals('('))
						{
							STATE = 36;
						}

						// -> (37)
						else if (CURRENT_CHAR.Equals(')'))
						{
							STATE = 37;
						}

						// -> (38)
						else if (CURRENT_CHAR.Equals(','))
						{
							STATE = 38;
						}

						// -> (39)
						else if (CURRENT_CHAR.Equals(';'))
						{
							STATE = 39;
						}

						// NONE
						else
						{
							Environment.Exit(0);
						}
					}
					break;

					
					// State 1
					case 1:
					{
						// ->> 1
						if (Char.IsDigit(CURRENT_CHAR))
						{
							STATE = 1;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 3
						else if (CURRENT_CHAR.Equals('.'))
						{
							STATE = 3;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (2) [Other]
						else
						{
							STATE = 2;
						}
					}
					break;

					
					// State 2 [It's a fucking final state!]
					case 2:
					{
						Restart();

						return new Token(Tag.CON_NUM, GetLexeme(), ROW, COLUMN);
					}

					
					// State 3
					case 3:
					{
						// -> 4
						if (Char.IsDigit(CURRENT_CHAR))
						{
							STATE = 4;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 0 [Error]
						else
						{
							STATE = 0;
							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;
					
					
					// State 4
					case 4:
					{
						// ->> 4
						if (Char.IsDigit(CURRENT_CHAR))
						{
							STATE = 4;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (5) [Other]
						else
						{
							STATE = 5;
						}
					}
					break;

					
					// State 5 [It's a fucking final state!]
					case 5:
					{
						Restart();

						return new Token(Tag.CON_NUM, GetLexeme(), ROW, COLUMN);
					}
					
					
					// State 6
					case 6:
					{
						// -> 7
						if (IsASCII(CURRENT_CHAR))
						{
							STATE = 7;
							LEXEME.Append(CURRENT_CHAR);
						}
						
						// -> 0 [Error]
						else
						{
							STATE = 0;
							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;

					
					// State 7
					case 7:
					{
						// -> (8)
						if (CURRENT_CHAR.Equals('\''))
						{
							STATE = 8;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 0 [Error]
						else
						{
							STATE = 0;
							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 8 [It's a fucking final state!]
					case 8:
					{
						STATE = 0;

						return new Token(Tag.CON_CHAR, GetLexeme(), ROW, COLUMN);
					}
					
					
					// State 9
					case 9:
					{
						// -> 10
						if (IsASCII(CURRENT_CHAR))
						{
							STATE = 10;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 0 [Error]
						else
						{
							STATE = 0;
							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 10
					case 10:
					{
						// ->> 10
						if (IsASCII(CURRENT_CHAR))
						{
							STATE = 10;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (11)
						else if (CURRENT_CHAR.Equals('\"'))
						{
							STATE = 11;
							LEXEME.Append(CURRENT_CHAR);
						}
					}
					break;


					// State 11 [It's a fucking final state!]
					case 11:
					{
						STATE = 0;

						return new Token(Tag.LIT, GetLexeme(), ROW, COLUMN);
					}


					// State 12
					case 12:
					{
						// ->> 12
						if (Char.IsLetter(CURRENT_CHAR) || Char.IsDigit(CURRENT_CHAR))
						{
							STATE = 12;
							LEXEME.Append(CURRENT_CHAR);

							if (Grammar.GetToken(GetLexeme()) != null)
							{
								return Grammar.GetToken(GetLexeme());
							}
						}

						// -> (13) [Other]
						else
						{
							STATE = 13;
						}
					}
					break;



					// State 13 [It's a fucking final state!]
					case 13:
					{
						Restart();

						return new Token(Tag.ID, GetLexeme(), ROW, COLUMN);
					}


					// State 14
					case 14:
					{
						// -> (15)
						if (CURRENT_CHAR.Equals('='))
						{
							STATE = 15;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (16)
						else
						{
							STATE = 16;
						}
					}
					break;


					// State 15 [It's a fucking final state!]
					case 15:
					{
						STATE = 0;

						return new Token(Tag.OP_EQ, GetLexeme(), ROW, COLUMN);
					}


					// State 16 [It's a fucking final state!]
					case 16:
					{
						Restart();

						return new Token(Tag.OP_ASS, GetLexeme(), ROW, COLUMN);
					}


					// State 17
					case 17:
					{
						// -> (18)
						if (CURRENT_CHAR.Equals('='))
						{
							STATE = 18;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (19)
						else
						{
							STATE = 19;
						}
					}
					break;


					// State 18 [It's a fucking final state!]
					case 18:
					{
						STATE = 0;

						return new Token(Tag.OP_GE, GetLexeme(), ROW, COLUMN);
					}


					// State 19 [It's a fucking final state!]
					case 19:
					{
						Restart();

						return new Token(Tag.OP_GT, GetLexeme(), ROW, COLUMN);
					}
					
					
					// State 20
					case 20:
					{
						// -> (21)
						if (CURRENT_CHAR.Equals('='))
						{
							STATE = 21;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (22)
						else
						{
							STATE = 22;
						}
					}
					break;


					// State 21 [It's a fucking final state!]
					case 21:
					{
						STATE = 0;

						return new Token(Tag.OP_LE, GetLexeme(), ROW, COLUMN);
					}


					// State 22 [It's a fucking final state!]
					case 22:
					{
						Restart();

						return new Token(Tag.OP_LT, GetLexeme(), ROW, COLUMN);
					}
					
					
					// State 23
					case 23:
					{
						// -> (24)
						if (CURRENT_CHAR.Equals('='))
						{
							STATE = 24;
							LEXEME.Append(CURRENT_CHAR);
						}

						// [Error]
						else
						{
							LexicalError("Incomplete token for the symbol ! " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 24 [It's a fucking final state!]
					case 24:
					{
						STATE = 0;

						return new Token(Tag.OP_NE, GetLexeme(), ROW, COLUMN);
					}


					// State 25
					case 25:
					{
						// -> 27
						if (CURRENT_CHAR.Equals('*'))
						{
							STATE = 27;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (26)
						else if (CURRENT_CHAR.Equals('/'))
						{
							STATE = 26;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> (30) [Other]
						else
						{
							STATE = 30;
						}
					}
					break;


					// State 26 [It's a fucking final state!]
					case 26:
					{
						// ->> (26)
						if (IsASCII(CURRENT_CHAR))
						{
							STATE = 26;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 0
						else
						{
							STATE = 0;

							return new Token(Tag.COM_ONL, GetLexeme(), ROW, COLUMN);
						}
					}
					break;


					// State 27
					case 27:
					{
						// -> 28
						if (CURRENT_CHAR.Equals('*'))
						{
							STATE = 28;
							LEXEME.Append(CURRENT_CHAR);
						}

						// ->> 27
						else if (IsASCII(CURRENT_CHAR))
						{
							STATE = 27;
							LEXEME.Append(CURRENT_CHAR);
						}
					}
					break;
					

					// State 28
					case 28:
					{
						// -> (29)
						if (CURRENT_CHAR.Equals('/'))
						{
							STATE = 29;
							LEXEME.Append(CURRENT_CHAR);
						}

						// ->> 28
						else if (CURRENT_CHAR.Equals('*'))
						{
							STATE = 28;
							LEXEME.Append(CURRENT_CHAR);
						}

						// -> 27
						else if (IsASCII(CURRENT_CHAR))
						{
							STATE = 27;
							LEXEME.Append(CURRENT_CHAR);
						}
					}
					break;


					// State 29 [It's a fucking final state!]
					case 29:
					{
						STATE = 0;

						return new Token(Tag.COM_CML, GetLexeme(), ROW, COLUMN);
					}


					// State 30 [It's a fucking final state!]
					case 30:
					{
						Restart();

						return new Token(Tag.OP_DIV, GetLexeme(), ROW, COLUMN);
					}


					// State 31 [It's a fucking final state!]
					case 31:
					{
						STATE = 0;

						return new Token(Tag.OP_MUL, GetLexeme(), ROW, COLUMN);
					}


					// State 32 [It's a fucking final state!]
					case 32:
					{
						STATE = 0;

						return new Token(Tag.OP_AD, GetLexeme(), ROW, COLUMN);
					}


					// State 33 [It's a fucking final state!]
					case 33:
					{
						STATE = 0;

						return new Token(Tag.OP_MIN, GetLexeme(), ROW, COLUMN);
					}


					// State 34 [It's a fucking final state!]
					case 34:
					{
						STATE = 0;

						return new Token(Tag.SMB_OBC, GetLexeme(), ROW, COLUMN);
					}


					// State 35 [It's a fucking final state!]
					case 35:
					{
						STATE = 0;

						return new Token(Tag.SMB_CBC, GetLexeme(), ROW, COLUMN);
					}


					// State 36 [It's a fucking final state!]
					case 36:
					{
						STATE = 0;

						return new Token(Tag.SMB_OPA, GetLexeme(), ROW, COLUMN);
					}


					// State 37 [It's a fucking final state!]
					case 37:
					{
						STATE = 0;

						return new Token(Tag.SMB_CPA, GetLexeme(), ROW, COLUMN);
					}


					// State 38 [It's a fucking final state!]
					case 38:
					{
						STATE = 0;

						return new Token(Tag.SMB_COM, GetLexeme(), ROW, COLUMN);
					}


					// State 39 [It's a fucking final state!]
					case 39:
					{
						STATE = 0;

						return new Token(Tag.SMB_SEM, GetLexeme(), ROW, COLUMN);
					}
				}
			}
		}




		// Getters and Setters
		public static string GetLexeme()
		{
			return LEXEME.ToString();
		}
	}
}
