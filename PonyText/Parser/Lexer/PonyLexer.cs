// File: PonyLexer.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.IO;
using System.Text;
using PonyText.Common.Parser;
using PonyText.Common.Parser.EventListener;
using PonyText.Parser.EventListener;
using PonyText.Parser.Tokens;

namespace PonyText.Parser.Lexer
{
    public class PonyLexer : ITokenGenerator, IErrorSource
    {
        private int[,] LiteralTransitionTable = new int[,]
        {
           // ANY, \, $, %
             { 0 , 1, 2, 3},
             {-1 , 0, 0, 0}
        };

        private int[,] CommandTransitionTable = new int[,]
        {
            //  0   1    2  3 4   5 6  7   8  9  10  11 12 13 14  15  16
            // SYM AZaz 0-9 - nr  " .   ]  \  %  $   LF _  SP CR ANY  #
            {   0,   3,  5, 4, 3, 1,-1, 7, 8, 9, 7,  0, 3, 0, 10, -1, 12},     //0
            {   1,   1,  1, 1, 1, 0, 1, 1, 2, 1, 1, -1, 1, 1, -1,  1,  1},     //1
            {  -1,  -1, -1,-1, 1, 1,-1,-1, 1,-1,-1, -1,-1, 0, -1, -1, -1},     //2
            {   0,   3,  3, 4, 3, 1,-1, 7, 8, 9, 7,  0, 3, 0, 10, -1, -1},     //3
            {  -1,  -1,  5,-1,-1,-1,-1,-1,-1,-1,-1, -1,-1, 0, -1, -1, -1},     //4
            {   0,   3,  5,-1, 3, 1, 6, 7, 8, 9, 7,  0, 3, 0, 10, -1, -1},     //5
            {   0,   3,  6,-1, 3, 1,-1, 7, 8, 9, 7,  0, 3, 0, 10, -1, -1},     //6
            {  -1,  -1, -1,-1,-1,-1,-1,-1,-1,-1,-1, -1,-1, 0, -1, -1, -1},     //7 POP context
            {  -1,  -1, -1,-1,-1,-1,-1,-1,-1,-1,-1,  0,-1, 0, 11, -1, -1},     //8
            {  -1,  -1, -1,-1,-1,-1,-1,-1,-1,-1,-1, -1,-1, 0, -1, -1, -1},     //9 PUSH literal context
            {  -1,  -1, -1,-1,-1,-1,-1,-1,-1,-1,-1,  0,-1,-1, -1, -1, -1},     //10 CR
            {  -1,  -1, -1,-1,-1,-1,-1,-1,-1,-1,-1,  0,-1,-1, -1, -1, -1},     //11 CR (escaped)
            {  12,  12, 12,12,12,12,12,12,12,12,12,  0,12,12, 12, 12, 12},     //12 comment
        };

        StringReader inputStream;
        PonyLexerContextManager contextManager;
        int col = 0, row = 1;
        bool isHalted = false;

        public event TokenGenerated onTokenGenerated;

        public IErrorListener errorListener
        {
            get; set;
        }

        public PonyLexer()
        {
            contextManager = new PonyLexerContextManager();
        }

        private bool literalRecognizer(int state)
        {
            int currentState = state;
            StringBuilder builder = new StringBuilder();
            char chr = '\0';
            while (currentState >= 0 && currentState < 2 && inputStream.Peek() != -1 && !isHalted)
            {
                chr = (char)inputStream.Read();
                int nextState = LiteralTransitionTable[currentState, literalMapper(chr)];
                if (nextState > 1)
                {
                    if (!string.IsNullOrWhiteSpace(builder.ToString()))
                    {
                        onTokenGenerated?.Invoke(new PonyToken(builder.ToString(), PonyTokenType.LITERAL, col, row));
                    }
                    onTokenGenerated?.Invoke(TokenHelper.GetSymbolToken(chr, col, row));
                }
                else if (nextState == 0)
                {
                    if (chr == '\n')
                    {
                        if (!string.IsNullOrWhiteSpace(builder.ToString()))
                        {
                            onTokenGenerated?.Invoke(new PonyToken(builder.ToString(), PonyTokenType.LITERAL, col, row));
                            builder.Clear();
                        }
                        // end of a paragraph
                        onTokenGenerated?.Invoke(new PonyToken(PonyTokenType.CRLF, col, row));
                        row++;
                        col = 0;
                    }
                    else if (chr != '\r')
                    {
                        // For CRLF ending system, do not record CR
                        builder.Append(chr);
                    }
                }
                currentState = nextState;
                col++;
            }
            if (currentState == 2)
            {
                contextManager.PushContext(new PonyLexerContext(0, commandRecognizer));
            }
            else if (currentState == 3)
            {
                contextManager.PopContext();
            }
            else if (currentState == -1)
            {
                errorListener?.OnLexerErrorReported("Unable to find a valid transition", chr.ToString(), col, row);
                return false;
            }
            return true;
        }

        private bool commandRecognizer(int state)
        {
            int currentState = state;
            StringBuilder builder = new StringBuilder();
            char chr = '\0';
            while (currentState != -1 && inputStream.Peek() != -1 && !(currentState == 7 || currentState == 9) && !isHalted)
            {
                chr = (char)inputStream.Read();
                int col_sel = commandMapper(chr);
                int nextState = CommandTransitionTable[currentState, col_sel];

                if (nextState == 12)
                {
                    currentState = nextState;
                    col++;
                    continue;
                }

                if (nextState == 3 || nextState == 1 || (4 <= nextState && nextState <= 6))
                {
                    builder.Append(chr);
                }
                else if (currentState == 3 && nextState != currentState)
                {
                    onTokenGenerated?.Invoke(new PonyToken(builder.ToString(), PonyTokenType.ID, col, row));
                    builder.Clear();
                }
                else if (currentState == 1 && nextState == 0)
                {
                    builder.Append(chr);
                    onTokenGenerated?.Invoke(new PonyToken(builder.ToString(), PonyTokenType.QUOTE, col, row));
                    builder.Clear();
                }
                else if (4 <= currentState && currentState <= 6 && !(4 <= nextState && nextState <= 6))
                {
                    onTokenGenerated?.Invoke(new PonyToken(builder.ToString(), PonyTokenType.NUMBER, col, row));
                    builder.Clear();
                }

                if (chr == '\n')
                {
                    row++;
                    col = 0;
                }
                if ((col_sel == 0 || 9 <= col_sel && col_sel <= 11 || col_sel == 7) &&
                    (nextState == 0 || nextState == 7 || nextState == 9) && currentState != 11)
                {
                    //So we don't report a token for any quoted character or one consider to be ID.
                    onTokenGenerated?.Invoke(TokenHelper.GetSymbolToken(chr, col, row));
                }
                currentState = nextState;
                col++;
            }

            if (currentState == 7)
            {
                contextManager.PopContext();
            }
            else if (currentState == 9)
            {
                contextManager.PushContext(new PonyLexerContext(0, literalRecognizer));
            }
            else if (currentState == -1)
            {
                errorListener?.OnLexerErrorReported("Unable to find a valid transition", chr.ToString(), col, row);
                return false;
            }
            return true;
        }

        private int literalMapper(char chr)
        {
            switch (chr)
            {
                case '\\':
                    return 1;
                case '$':
                    return 2;
                case '%':
                    return 3;
                default:
                    return 0;
            }

        }

        private int commandMapper(char chr)
        {
            switch (chr)
            {
                case '{':
                case '[':
                case '}':
                case ')':
                case '(':
                case '@':
                case ':':
                    return 0;
                case '-':
                    return 3;
                case 'n':
                case 'r':
                    return 4;
                case '"':
                    return 5;
                case '.':
                    return 6;
                case ']':
                    return 7;
                case '\\':
                    return 8;
                case '%':
                    return 9;
                case '$':
                    return 10;
                case '\n':
                    return 11;
                case '_':
                    return 12;
                case '\t':
                case ' ':
                    return 13;
                case '\r':
                    return 14;
                case '#':
                    return 16;
            }

            if (char.IsLetter(chr))
            {
                return 1;
            }
            else if (char.IsDigit(chr))
            {
                return 2;
            }
            return 15;
        }

        public void HaltGenerator()
        {
            isHalted = true;
        }

        public bool RunGenerator(string text)
        {
            bool result = true;
            inputStream = new StringReader(text);
            contextManager.PushContext(new PonyLexerContext(0, literalRecognizer));

            while (inputStream.Peek() != -1 && !contextManager.IsEmpty() && result && !isHalted)
            {
                PonyLexerContext context = contextManager.CurrentContext;
                result = context.DFAHandler.Invoke(context.CurrentState);
            }

            result = result && !isHalted;

            if (!result || contextManager.IsEmpty())
            {
                errorListener?.OnLexerErrorReported("Lexer terminated unexpected", string.Empty, col, row);
            }

            return result;
        }

        public int GetCurrentRow()
        {
            return row;
        }

        public int GetCurrentColumn()
        {
            return col;
        }
    }
}
