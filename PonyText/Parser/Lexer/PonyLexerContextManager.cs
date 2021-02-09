// File: PonyLexerContextManager.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;

namespace PonyText.Parser.Lexer
{
    public class PonyLexerContextManager
    {
        //public const ushort LiteralContext = 0xf000;
        //public const ushort CommandContext = 0xe000;

        private Stack<PonyLexerContext> contextStack;

        public PonyLexerContext CurrentContext
        {
            get;
            private set;
        }

        public PonyLexerContextManager()
        {
            contextStack = new Stack<PonyLexerContext>();
        }

        public void PushContext(PonyLexerContext newContext)
        {
            CurrentContext = newContext;
            contextStack.Push(newContext);
        }

        public void PopContext()
        {
            contextStack.Pop();
            CurrentContext = contextStack.Peek();
        }

        public void SwitchContext(PonyLexerContext newContext)
        {
            contextStack.Pop();
            contextStack.Push(newContext);
            CurrentContext = newContext;
        }

        public bool IsEmpty() => contextStack.Count == 0;

    }
}
