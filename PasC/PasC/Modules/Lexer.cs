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
		private static int ROW = 1;
		private static int COLUMN = 1;
		private static Token LAST_TOKEN;
		private static char CURRENT_CHAR;
		private static StringBuilder LEXEME;

		// File Pointers
		private static int LAST_CHAR = 0;
		private static readonly int EOF = -1;

		// Check
		private static int STATE;

		// Source
		private static FileStream sourceFile;




		// Source Control
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

		private static void Read()
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

		private static void Restart()
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




		// Support
		private static void LexicalError(String message)
		{
			Console.WriteLine("[Lexical Error]: " + message + "\n");
		}

		private static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), "[\x00-\xFF]");
		}




		// Pasc:Automata
		private static Token NextToken()
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
							SetState(0, false);
						}

						// ->> 0
						else if (CURRENT_CHAR == '\n' || CURRENT_CHAR == '\r')
						{
							SetState(0, false);

							CURRENT_CHAR = (char) sourceFile.ReadByte();
						}

						// -> 1
						else if (Char.IsDigit(CURRENT_CHAR))
						{
							SetState(1, true);
						}

						// -> 6
						else if (CURRENT_CHAR.Equals('\''))
						{
							SetState(6, true);
						}

						// -> 9
						else if (CURRENT_CHAR.Equals('\"'))
						{
							SetState(9, true);
						}

						// -> 12
						else if (Char.IsLetter(CURRENT_CHAR))
						{
							SetState(12, true);
						}

						// -> 14
						else if (CURRENT_CHAR.Equals('='))
						{
							SetState(14, true);
						}

						// -> 17
						else if (CURRENT_CHAR.Equals('>'))
						{
							SetState(17, true);
						}

						// -> 20
						else if (CURRENT_CHAR.Equals('<'))
						{
							SetState(20, true);
						}

						// -> 23
						else if (CURRENT_CHAR.Equals('!'))
						{
							SetState(23, true);
						}

						// -> 25
						else if (CURRENT_CHAR.Equals('/'))
						{
							SetState(25, true);
						}

						// -> (31)
						else if (CURRENT_CHAR.Equals('*'))
						{
							SetState(31, true);
						}

						// -> (32)
						else if (CURRENT_CHAR.Equals('+'))
						{
							SetState(32, true);
						}

						// -> (33)
						else if (CURRENT_CHAR.Equals('-'))
						{
							SetState(33, true);
						}

						// -> (34)
						else if (CURRENT_CHAR.Equals('{'))
						{
							SetState(34, true);
						}

						// -> (35)
						else if (CURRENT_CHAR.Equals('}'))
						{
							SetState(35, true);
						}

						// -> (36)
						else if (CURRENT_CHAR.Equals('('))
						{
							SetState(36, true);
						}

						// -> (37)
						else if (CURRENT_CHAR.Equals(')'))
						{
							SetState(37, true);
						}

						// -> (38)
						else if (CURRENT_CHAR.Equals(','))
						{
							SetState(38, true);
						}

						// -> (39)
						else if (CURRENT_CHAR.Equals(';'))
						{
							SetState(39, true);
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
							SetState(1, true);
						}

						// -> 3
						else if (CURRENT_CHAR.Equals('.'))
						{
							SetState(3, true);
						}

						// -> (2) [Other]
						else
						{
							SetState(2, false);
						}
					}
					break;

					
					// State 2 [FINAL STATE]
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
							SetState(4, true);
						}

						// -> 0 [Error]
						else
						{
							SetState(0, false);

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
							SetState(4, true);
						}

						// -> (5) [Other]
						else
						{
							SetState(5, false);
						}
					}
					break;

					
					// State 5 [FINAL STATE]
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
							SetState(7, true);
						}
						
						// -> 0 [Error]
						else
						{
							SetState(0, false);

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
							SetState(8, true);
						}

						// -> 0 [Error]
						else
						{
							SetState(0, false);

							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 8 [FINAL STATE]
					case 8:
					{
						SetState(0, false);

						return new Token(Tag.CON_CHAR, GetLexeme(), ROW, COLUMN);
					}
					
					
					// State 9
					case 9:
					{
						// -> 10
						if (IsASCII(CURRENT_CHAR))
						{
							SetState(10, true);
						}

						// -> 0 [Error]
						else
						{
							SetState(0, false);

							LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 10
					case 10:
					{
                            // -> (11)
					    if (CURRENT_CHAR.Equals('\"'))
						{
							SetState(11, true);
						}

						// ->> 10
						else if (IsASCII(CURRENT_CHAR))
						{
							SetState(10, true);
						}
					}
					break;


					// State 11 [FINAL STATE]
					case 11:
					{
						SetState(0, false);

						return new Token(Tag.LIT, GetLexeme(), ROW, COLUMN);
					}


					// State 12
					case 12:
					{
						// ->> 12
						if (Char.IsLetter(CURRENT_CHAR) || Char.IsDigit(CURRENT_CHAR))
						{
							SetState(12, true);

							if (Grammar.GetToken(GetLexeme()) != null)
							{
								return Grammar.GetToken(GetLexeme());
							}
						}

						// -> (13) [Other]
						else
						{
							SetState(13, false);
						}
					}
					break;



					// State 13 [FINAL STATE]
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
							SetState(15, true);
						}

						// -> (16) [Other]
						else
						{
							SetState(16, false);
						}
					}
					break;


					// State 15 [FINAL STATE]
					case 15:
					{
						SetState(0, false);

						return new Token(Tag.OP_EQ, GetLexeme(), ROW, COLUMN);
					}


					// State 16 [FINAL STATE]
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
							SetState(18, true);
						}

						// -> (19) [Other]
						else
						{
							SetState(19, false);
						}
					}
					break;


					// State 18 [FINAL STATE]
					case 18:
					{
						SetState(0, false);

						return new Token(Tag.OP_GE, GetLexeme(), ROW, COLUMN);
					}


					// State 19 [FINAL STATE]
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
							SetState(21, true);
						}

						// -> (22) [Other]
						else
						{
							SetState(22, false);
						}
					}
					break;


					// State 21 [FINAL STATE]
					case 21:
					{
						SetState(0, false);

						return new Token(Tag.OP_LE, GetLexeme(), ROW, COLUMN);
					}


					// State 22 [FINAL STATE]
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
							SetState(24, true);
						}

						// -> 0 [Error]
						else
						{
							SetState(0, false);

							LexicalError("Incomplete token for the symbol ! " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
						}
					}
					break;


					// State 24 [FINAL STATE]
					case 24:
					{
						SetState(0, false);

						return new Token(Tag.OP_NE, GetLexeme(), ROW, COLUMN);
					}


					// State 25
					case 25:
					{
						// -> 27
						if (CURRENT_CHAR.Equals('*'))
						{
							SetState(27, true);
						}

						// -> (26)
						else if (CURRENT_CHAR.Equals('/'))
						{
							SetState(26, true);
						}

						// -> (30) [Other]
						else
						{
							SetState(30, false);
						}
					}
					break;


					// State 26 [FINAL STATE]
					case 26:
					{
						// ->> (26)
						if (IsASCII(CURRENT_CHAR))
						{
							SetState(26, true);
						}

						// -> 0
						else
						{
							SetState(0, false);

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
							SetState(28, true);
						}

						// ->> 27
						else if (IsASCII(CURRENT_CHAR))
						{
							SetState(27, true);
						}
					}
					break;
					

					// State 28
					case 28:
					{
						// -> (29)
						if (CURRENT_CHAR.Equals('/'))
						{
							SetState(29, true);
						}

						// ->> 28
						else if (CURRENT_CHAR.Equals('*'))
						{
							SetState(28, true);
						}

						// -> 27
						else if (IsASCII(CURRENT_CHAR))
						{
							SetState(27, true);
						}
					}
					break;


					// State 29 [FINAL STATE]
					case 29:
					{
						SetState(0, false);

						return new Token(Tag.COM_CML, GetLexeme(), ROW, COLUMN);
					}


					// State 30 [FINAL STATE]
					case 30:
					{
						Restart();

						return new Token(Tag.OP_DIV, GetLexeme(), ROW, COLUMN);
					}


					// State 31 [FINAL STATE]
					case 31:
					{
						SetState(0, false);

						return new Token(Tag.OP_MUL, GetLexeme(), ROW, COLUMN);
					}


					// State 32 [FINAL STATE]
					case 32:
					{
						SetState(0, false);

						return new Token(Tag.OP_AD, GetLexeme(), ROW, COLUMN);
					}


					// State 33 [FINAL STATE]
					case 33:
					{
						SetState(0, false);

						return new Token(Tag.OP_MIN, GetLexeme(), ROW, COLUMN);
					}


					// State 34 [FINAL STATE]
					case 34:
					{
						SetState(0, false);

						return new Token(Tag.SMB_OBC, GetLexeme(), ROW, COLUMN);
					}


					// State 35 [FINAL STATE]
					case 35:
					{
						SetState(0, false);

						return new Token(Tag.SMB_CBC, GetLexeme(), ROW, COLUMN);
					}


					// State 36 [FINAL STATE]
					case 36:
					{
						SetState(0, false);

						return new Token(Tag.SMB_OPA, GetLexeme(), ROW, COLUMN);
					}


					// State 37 [FINAL STATE]
					case 37:
					{
						SetState(0, false);

						return new Token(Tag.SMB_CPA, GetLexeme(), ROW, COLUMN);
					}


					// State 38 [FINAL STATE]
					case 38:
					{
						SetState(0, false);

						return new Token(Tag.SMB_COM, GetLexeme(), ROW, COLUMN);
					}


					// State 39 [FINAL STATE]
					case 39:
					{
						SetState(0, false);

						return new Token(Tag.SMB_SEM, GetLexeme(), ROW, COLUMN);
					}
				}
			}
		}




		// Getters and Setters
		private static void SetState(int currentState, bool appendLexeme)
		{
			STATE = currentState;

			if (appendLexeme)
			{
				LEXEME.Append(CURRENT_CHAR);
			}
		}

		public static string GetLexeme()
		{
			return LEXEME.ToString();
		}
	}
}
