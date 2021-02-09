// File: TokenHelper.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Parser;
using PonyText.Parser.Tokens;

namespace PonyText.Parser.Lexer
{
    public class TokenHelper
    {
        public static PonyToken GetSymbolToken(char symbol, int col, int row)
        {
            switch (symbol)
            {
                case '{':
                    return new PonyToken(PonyTokenType.LBRACE, col, row);
                case '}':
                    return new PonyToken(PonyTokenType.RBRACE, col, row);
                case '[':
                    return new PonyToken(PonyTokenType.LBRACKET, col, row);
                case ']':
                    return new PonyToken(PonyTokenType.RBRACKET, col, row);
                case '(':
                    return new PonyToken(PonyTokenType.LPAREN, col, row);
                case ')':
                    return new PonyToken(PonyTokenType.RPAREN, col, row);
                case '@':
                    return new PonyToken(PonyTokenType.AT, col, row);
                case '$':
                    return new PonyToken(PonyTokenType.DOLLAR, col, row);
                case '%':
                    return new PonyToken(PonyTokenType.PERCENTAGE, col, row);
                case ':':
                    return new PonyToken(PonyTokenType.COLON, col, row);
                case '\n':
                    return new PonyToken(PonyTokenType.CRLF, col, row);
                default:
                    return new PonyToken(symbol.ToString(), PonyTokenType.UNKNOW, col, row);
            }
        }
    }
}
