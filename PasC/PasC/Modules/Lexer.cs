using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using PasC.Models;

namespace PasC.Modules
{
	class Lexer
	{
		// Lexeme
		private static char CURRENT_CHAR;
		private static StringBuilder LEXEME;

		// File Pointers
		private static int ROW = 1;
		private static int COLUMN = 1;
		private static int LAST_CHAR = 0;
		private static readonly int EOF = -1;

		// Check
		private static int STATE;
		private static bool QUOTES_ERROR = false;
		



		// Lexer Control
		private static void Read()
		{
			CURRENT_CHAR = '\u0000';

			try
			{
				LAST_CHAR = Global.SOURCE.ReadByte();

				if (LAST_CHAR != EOF)
				{
					CURRENT_CHAR = (char)LAST_CHAR;

					if (IsNewLine())
					{
						ROW++;
						COLUMN = 1;
					}

					else if (CURRENT_CHAR == '\t')
					{
						COLUMN += 3;
					}
					else if (!IsNewLine())
					{
						COLUMN++;
					}
				}
				else
				{
					MultilineCommentErrorCheck();

					AddToken(Tag.EOF);

					Global.SOURCE.Close();

                    Environment.Exit(0);
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
					Global.SOURCE.Seek(Global.SOURCE.Position - 1, SeekOrigin.Begin);

					COLUMN--;
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("[Error]: Failed to read the source file.\n{0}", e);

				Environment.Exit(2);
			}
		}

		private static Token AddToken(Tag tag)
		{
			Token TOKEN = new Token(tag, GetLexeme(), ROW, COLUMN);

			Grammar.Add(TOKEN, new Identifier());

			return TOKEN;
		}




		// Check
		private static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), @"[\x20-\xFF]");
		}

		private static bool IsNewLine()
		{
			return CURRENT_CHAR == Global.NEW_LINE;
		}




		// Errors
		private static void LexicalError(String message)
		{
			Console.WriteLine("\n[LEXICAL ERROR]: " + message.Replace("\n", "<LINE_BREAK>").Replace("\r", "<LINE_BREAK>").Replace("\t", "<TAB>") + "\n");
		}

		private static void MultilineCommentErrorCheck()
		{
			if (STATE.Equals(27) || STATE.Equals(28))
			{
				Console.WriteLine("\n[LEXICAL ERROR]: Multiline comment not closed on line {0}.", ROW - 1);
			}
		}




		// PasC:Automata
		public static Token NextToken()
		{
			STATE = 0;

			LEXEME = new StringBuilder();

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
						else if (IsNewLine())
						{
							SetState(0, false);
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
							SetState(0, true);

							return AddToken(Tag.OP_MUL);
						}

						// -> (32)
						else if (CURRENT_CHAR.Equals('+'))
						{
							SetState(0, true);

							return AddToken(Tag.OP_AD);
						}

						// -> (33)
						else if (CURRENT_CHAR.Equals('-'))
						{
							SetState(0, true);

							return AddToken(Tag.OP_MIN);
						}

						// -> (34)
						else if (CURRENT_CHAR.Equals('{'))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_OBC);
						}

						// -> (35)
						else if (CURRENT_CHAR.Equals('}'))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_CBC);
						}

						// -> (36)
						else if (CURRENT_CHAR.Equals('('))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_OPA);
						}

						// -> (37)
						else if (CURRENT_CHAR.Equals(')'))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_CPA);
						}

						// -> (38)
						else if (CURRENT_CHAR.Equals(','))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_COM);
						}

						// -> (39)
						else if (CURRENT_CHAR.Equals(';'))
						{
							SetState(0, true);

							return AddToken(Tag.SMB_SEM);
						}

						// NONE
						else
						{
							LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
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
							Restart();

							return AddToken(Tag.CON_NUM);
						}
					}
					break;

					
					// State 3
					case 3:
					{
						// -> 4
						if (Char.IsDigit(CURRENT_CHAR))
						{
							SetState(4, true);
						}

						// -> [Error]
						else
						{
							LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
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
							Restart();

							return AddToken(Tag.CON_NUM);
						}
					}
					break;
					
					
					// State 6
					case 6:
					{
						// -> 7
						if (IsASCII(CURRENT_CHAR))
						{
							SetState(7, true);
						}
						
						// -> [Error]
						else
						{
							LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
						}
					}
					break;

					
					// State 7
					case 7:
					{
						// -> (8)
						if (CURRENT_CHAR.Equals('\''))
						{
							SetState(0, true);

							return AddToken(Tag.CON_CHAR);
						}

						// -> [Error]
						else
						{
							LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
						}
					}
					break;
					
					
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

							LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
						}
					}
					break;


					// State 10
					case 10:
					{
						// -> (11)
						if (CURRENT_CHAR.Equals('\"'))
						{
							SetState(0, true);

							QUOTES_ERROR = false;

							return AddToken(Tag.LIT);
						}

						else if (IsNewLine() && !QUOTES_ERROR)
						{
							SetState(10, false);

							QUOTES_ERROR = true;

							LexicalError(String.Format("Missed closing quotes on line {0} and column {1}.", ROW - 1, COLUMN));
						}

						// ->> 10
						else if (IsASCII(CURRENT_CHAR))
						{
							if (!QUOTES_ERROR)
							{
								SetState(10, true);
							}
							else
							{
								LexicalError(String.Format("Invalid character '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
							}
						}
					}
					break;


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
							Restart();

							return AddToken(Tag.ID);
						}
					}
					break;


					// State 14
					case 14:
					{
						// -> (15)
						if (CURRENT_CHAR.Equals('='))
						{
							SetState(0, true);

							return AddToken(Tag.OP_EQ);
						}

						// -> (16) [Other]
						else
						{
							Restart();

							return AddToken(Tag.OP_ASS);
						}
					}


					// State 17
					case 17:
					{
						// -> (18)
						if (CURRENT_CHAR.Equals('='))
						{
							SetState(0, true);

							return AddToken(Tag.OP_GE);
						}

						// -> (19) [Other]
						else
						{
							Restart();

							return AddToken(Tag.OP_GT);
						}
					}
					
					
					// State 20
					case 20:
					{
						// -> (21)
						if (CURRENT_CHAR.Equals('='))
						{
							SetState(0, true);

							return AddToken(Tag.OP_LE);
						}

						// -> (22) [Other]
						else
						{
							Restart();

							return AddToken(Tag.OP_LT);
						}
					}
					
					
					// State 23
					case 23:
					{
						// -> (24)
						if (CURRENT_CHAR.Equals('='))
						{
							SetState(0, true);

							return AddToken(Tag.OP_NE);
						}

						// -> 0 [Error]
						else
						{
							SetState(0, false);
							
							LexicalError(String.Format("Incomplete token for the symbol '{0}' on line {1} and column {2}.", CURRENT_CHAR, ROW, COLUMN));
						}
					}
					break;


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
							Restart();

							return AddToken(Tag.OP_DIV);
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

							// return AddToken(Tag.COM_ONL);
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
							SetState(0, true);

							// return AddToken(Tag.COM_CML);
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
