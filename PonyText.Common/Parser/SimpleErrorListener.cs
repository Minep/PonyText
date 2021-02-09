// File: SimpleErrorListener.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.IO;

namespace PonyText.Common.Parser.EventListener
{
    public class SimpleErrorListener : IErrorListener
    {
        StreamWriter outputStream;

        public SimpleErrorListener(StreamWriter outputStream)
        {
            this.outputStream = outputStream;
        }

        public void OnLexerErrorReported(string casue, string symbol, int column, int row)
        {
            outputStream.WriteLine("[ERROR](Lexer) At character '{1}' on {3}:{2}. {0}", casue, symbol, column, row);
        }

        public void OnParserErrorReported(string casue, int fromState, string token)
        {
            outputStream.WriteLine("[ERROR](Parser) At token {0} when performing transition on state {1}. {2}",
                                token, fromState, casue);
        }
    }
}
