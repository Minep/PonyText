// File: PonyToken.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

namespace PonyText.Common.Parser {
    public class PonyToken {

        public static readonly PonyToken Empty = new PonyToken(PonyTokenType.EMPTY, 0, 0);

        public static PonyToken Create(int col, int row, PonyTokenType type) {
            return new PonyToken(type, col, row);
        }
        public string Text {
            get;
        }

        public PonyTokenType TokenType {
            get;
        }

        public int Column {
            get;
        }

        public int Row {
            get;
        }

        public PonyToken(string text, PonyTokenType tokenType, int column, int row) {
            Text = text;
            TokenType = tokenType;
            Column = column;
            Row = row;
        }

        public PonyToken(PonyTokenType tokenType, int column, int row) {
            TokenType = tokenType;
            Column = column;
            Row = row;
        }

        public override string ToString() {
            return string.Format("<{0}@{1}:{2}, {3}>", TokenType, Row, Column, Text);
        }

        public PonyToken DeriveEmptyToken() {
            return new PonyToken(TokenType, Column, Row);
        }
    }
}
