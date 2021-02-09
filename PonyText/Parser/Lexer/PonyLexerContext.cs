// File: PonyLexerContext.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

namespace PonyText.Parser.Lexer
{
    public delegate bool DFAHandler(int currentState);
    public class PonyLexerContext
    {
        public int CurrentState
        {
            get;
        }

        public DFAHandler DFAHandler
        {
            get;
        }

        public PonyLexerContext(int currentState, DFAHandler dfaHandler)
        {
            CurrentState = currentState;
            DFAHandler = dfaHandler;
        }
    }
}
