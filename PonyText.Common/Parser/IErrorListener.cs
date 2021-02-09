// File: IErrorListener.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

namespace PonyText.Common.Parser.EventListener
{
    public interface IErrorListener
    {
        public void OnLexerErrorReported(string casue, string symbol, int column, int row);
        public void OnParserErrorReported(string casue, int fromState, string token);
    }
}
