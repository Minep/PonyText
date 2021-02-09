// File: PonyTokenType.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

namespace PonyText.Common.Parser {
    public enum PonyTokenType {
        LITERAL,
        LBRACE,
        RBRACE,
        LBRACKET,
        RBRACKET,
        LPAREN,
        RPAREN,
        AT,
        ID,
        CRLF,
        QUOTE,
        COLON,
        DOLLAR,
        PERCENTAGE,
        NUMBER,
        UNKNOW,
        EMPTY
    }
}
