using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using PasC.Models;

namespace PasC.States
{
	class Lexer
	{
		// Source Pointers
		public static int ROW = 1;
		public static int COLUMN = 1;
		public static char CURRENT_CHAR;
		private static StringBuilder LEXEME;
        public static Token lastToken;

		// File Pointers
		public static int LAST_CHAR = 0;
		public static readonly int EOF = -1;

		// Check
		public static bool FINAL_STATE;

		// Source
		private static FileStream sourceFile;




		public static void Set(string source)
		{
			LEXEME = new StringBuilder();
			sourceFile = new FileStream(source, FileMode.Open, FileAccess.Read);
            Token token;

            //State00.Run();
            do
            {
                // COLOCAR LOOP DA EXECUCAO
                LEXEME.Clear();
                token = NextToken();

                if (token != null)
                {
                    Console.WriteLine("Token: " + token.ToString() + "\t Line: " + ROW + "\t Column: " + COLUMN);
                }

                lastToken = token;

                if (Grammar.GetToken(GetLexeme()) == null && lastToken != null)
                {
                    Grammar.Add(lastToken, new Identifier());
                }               

            } while (!token.Lexeme.Equals(Tag.EOF) && token != null);
            sourceFile.Close();
		}

		public static void Read()
		{
			CURRENT_CHAR = '\u0000';

			if (FINAL_STATE)
			{
				FINAL_STATE = false;
				LEXEME = new StringBuilder();
			}

			try
			{
				LAST_CHAR = sourceFile.ReadByte();

				if (LAST_CHAR != EOF)
				{
					CURRENT_CHAR = (char) LAST_CHAR;
                    COLUMN++;

					while (CURRENT_CHAR == '\t' || CURRENT_CHAR == '\n' || CURRENT_CHAR == '\r')
                    {
						CURRENT_CHAR = (char) sourceFile.ReadByte();
					}

					//if (CURRENT_CHAR == '\t')
					//{
					//	LEXEME.Append(' ');
					//	LEXEME.Append(' ');
					//	LEXEME.Append(' ');
					//}
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

		public static void Fallback()
		{
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

		public static string GetLexeme()
		{
			return LEXEME.ToString();
		}

		public static void IsAFinalState()
		{
			FINAL_STATE = true;
		}

		public static bool IsASCII(char c)
		{
			return Regex.IsMatch(c.ToString(), "[\x00-\xFF]");
		}

        public static void LexicalError(String msg)
        {
            Console.WriteLine("[Lexical Error]: " + msg + "\n");
        }

        public static Token NextToken()
        {
            int state = 0;

            while (true)
            {
                Read();

                switch (state)
                {
                    case 0:
                        if (CURRENT_CHAR == '\t')
                        {
                            state = 0;
                        }
                        else if (Char.IsWhiteSpace(CURRENT_CHAR))
                        {
                            state = 0;
                        }
                        else if (Char.IsDigit(CURRENT_CHAR))
                        {
                            state = 1; // Digito
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('\''))
                        {
                            state = 6; // Aspas simples
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('\"'))
                        {
                            state = 9; // Aspas duplas
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (Char.IsLetter(CURRENT_CHAR))
                        {
                            state = 12;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('='))
                        {
                            state = 14;
                        }
                        else if (CURRENT_CHAR.Equals('>'))
                        {
                            state = 17;
                        }
                        else if (CURRENT_CHAR.Equals('<'))
                        {
                            state = 20;
                        }
                        else if (CURRENT_CHAR.Equals('!'))
                        {
                            state = 23;
                        }
                        else if (CURRENT_CHAR.Equals('/'))
                        {
                            state = 25;
                        }
                        else if (CURRENT_CHAR.Equals('*'))
                        {
                            state = 31;
                        }
                        else if (CURRENT_CHAR.Equals('+'))
                        {
                            state = 32;
                        }
                        else if (CURRENT_CHAR.Equals('-'))
                        {
                            state = 33;
                        }
                        else if (CURRENT_CHAR.Equals('{'))
                        {
                            state = 34;
                        }
                        else if (CURRENT_CHAR.Equals('}'))
                        {
                            state = 35;
                        }
                        else if (CURRENT_CHAR.Equals('('))
                        {
                            state = 36;
                        }
                        else if (CURRENT_CHAR.Equals(')'))
                        {
                            state = 37;
                        }
                        else if (CURRENT_CHAR.Equals(','))
                        {
                            state = 38;
                        }
                        else if (CURRENT_CHAR.Equals(';'))
                        {
                            state = 39;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                    case 1:
                        if (Char.IsDigit(CURRENT_CHAR))
                        {
                            state = 1; // Digito
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('.'))
                        {
                            state = 3; // Ponto
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 2; // Outro caractere
                        }
                        break;
                    case 2: // Estado final

                        // Reinicia automato
                        state = 0;
                        Fallback();

                        return new Token(Tag.CON_NUM, GetLexeme(), ROW, COLUMN); // retorna digito
                    case 3:
                        if (Char.IsDigit(CURRENT_CHAR))
                        {
                            state = 4; // Digito
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 0;
                            LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
                        }
                        break;
                    case 4:
                        if (Char.IsDigit(CURRENT_CHAR))
                        {
                            state = 4; // Digito
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 5; // Outro
                        }
                        break;
                    case 5:
                        state = 0;
                        Fallback();

                        return new Token(Tag.CON_NUM, GetLexeme(), ROW, COLUMN);
                    case 6:
                        if (IsASCII(CURRENT_CHAR))
                        {
                            state = 7; // Caractere
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 0;
                            LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
                        }
                        break;
                    case 7:
                        if (CURRENT_CHAR.Equals('\''))
                        {
                            state = 8;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 0;
                            LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
                        }
                        break;
                    case 8:
                        state = 0;

                        return new Token(Tag.CON_CHAR, GetLexeme(), ROW, COLUMN);
                    case 9:
                        if (IsASCII(CURRENT_CHAR))
                        {
                            state = 10;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 0;

                            LexicalError("Invalid character " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
                        }
                        break;
                    case 10:
                        if (IsASCII(CURRENT_CHAR))
                        {
                            state = 10;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('\"'))
                        {
                            state = 11;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        break;
                    case 11:
                        state = 0;

                        return new Token(Tag.LIT, GetLexeme(), ROW, COLUMN);
                    case 12:
                        if(Char.IsLetter(CURRENT_CHAR) || Char.IsDigit(CURRENT_CHAR))
                        {
                            state = 12;
                            LEXEME.Append(CURRENT_CHAR);

                            if (Grammar.GetToken(GetLexeme()) != null)
                            {
                                return Grammar.GetToken(GetLexeme());
                            }
                        }
                        else
                        {
                            state = 13;
                        }
                        break;
                    case 13:
                        state = 0;
                        Fallback();

                        return new Token(Tag.ID, GetLexeme(), ROW, COLUMN);
                    case 14:
                        if (CURRENT_CHAR.Equals('='))
                        {
                            state = 15;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 16;
                        }
                        break;
                    case 15:
                        state = 0;

                        return new Token(Tag.OP_EQ, GetLexeme(), ROW, COLUMN);
                    case 16:
                        state = 0;
                        Fallback();

                        return new Token(Tag.OP_ASS, GetLexeme(), ROW, COLUMN);
                    case 17:
                        if (CURRENT_CHAR.Equals('='))
                        {
                            state = 18;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 19;
                        }
                        break;
                    case 18:
                        state = 0;

                        return new Token(Tag.OP_GE, GetLexeme(), ROW, COLUMN);
                    case 19:
                        state = 0;
                        Fallback();

                        return new Token(Tag.OP_GT, GetLexeme(), ROW, COLUMN);
                    case 20:
                        if (CURRENT_CHAR.Equals('='))
                        {
                            state = 21;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 22;
                        }
                        break;
                    case 21:
                        state = 0;

                        return new Token(Tag.OP_LE, GetLexeme(), ROW, COLUMN);
                    case 22:
                        state = 0;
                        Fallback();

                        return new Token(Tag.OP_LT, GetLexeme(), ROW, COLUMN);
                    case 23:
                        if (CURRENT_CHAR.Equals('='))
                        {
                            state = 24;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            LexicalError("Incomplete token for the symbol ! " + CURRENT_CHAR + " on line " + ROW + " and column " + COLUMN);
                        }
                        break;
                    case 24:
                        state = 0;

                        return new Token(Tag.OP_NE, GetLexeme(), ROW, COLUMN);
                    case 25:
                        if (CURRENT_CHAR.Equals('*'))
                        {
                            state = 27;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('/'))
                        {
                            state = 26;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 30;
                        }
                        break;
                    case 26:
                        if (IsASCII(CURRENT_CHAR))
                        {
                            state = 26;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else
                        {
                            state = 0;

                            return new Token(Tag.COM_ONL, GetLexeme(), ROW, COLUMN);
                        }
                        break;
                    case 27:
                        if (CURRENT_CHAR.Equals('*'))
                        {
                            state = 28;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (IsASCII(CURRENT_CHAR))
                        {
                            state = 27;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        break;
                    case 28:
                        if (CURRENT_CHAR.Equals('/'))
                        {
                            state = 29;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (CURRENT_CHAR.Equals('*'))
                        {
                            state = 28;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        else if (IsASCII(CURRENT_CHAR))
                        {
                            state = 27;
                            LEXEME.Append(CURRENT_CHAR);
                        }
                        break;
                    case 29:
                        state = 0;

                        return new Token(Tag.COM_CML, GetLexeme(), ROW, COLUMN);
                    case 30:
                        state = 0;
                        Fallback();

                        return new Token(Tag.OP_DIV, GetLexeme(), ROW, COLUMN);
                    case 31:
                        state = 0;

                        return new Token(Tag.OP_MUL, GetLexeme(), ROW, COLUMN);
                    case 32:
                        state = 0;

                        return new Token(Tag.OP_AD, GetLexeme(), ROW, COLUMN);
                    case 33:
                        state = 0;

                        return new Token(Tag.OP_MIN, GetLexeme(), ROW, COLUMN);
                    case 34:
                        state = 0;

                        return new Token(Tag.SMB_OBC, GetLexeme(), ROW, COLUMN);
                    case 35:
                        state = 0;

                        return new Token(Tag.SMB_CBC, GetLexeme(), ROW, COLUMN);
                    case 36:
                        state = 0;

                        return new Token(Tag.SMB_OPA, GetLexeme(), ROW, COLUMN);
                    case 37:
                        state = 0;

                        return new Token(Tag.SMB_CPA, GetLexeme(), ROW, COLUMN);
                    case 38:
                        state = 0;

                        return new Token(Tag.SMB_COM, GetLexeme(), ROW, COLUMN);
                    case 39:
                        state = 0;

                        return new Token(Tag.SMB_SEM, GetLexeme(), ROW, COLUMN);
                }

                
            }
        }
	}
}
