// File: PonyTokenStream.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Parser;

namespace PonyText.Parser.Tokens
{
    public class PonyTokenStream
    {
        Queue<PonyToken> buffer;

        public PonyTokenStream()
        {
            buffer = new Queue<PonyToken>();
        }

        public void WriteToken(PonyToken token)
        {
            buffer.Enqueue(token);
        }

        public PonyToken ReadToken()
        {
            return buffer.Dequeue();
        }

        public bool IsEmpty()
        {
            return buffer.Count == 0;
        }
    }
}
