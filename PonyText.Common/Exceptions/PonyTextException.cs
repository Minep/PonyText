// File: PonyTextException.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections;

namespace PonyText.Common.Exceptions
{

    [Serializable]
    public class PonyTextException : Exception
    {
        protected ProcessingStage stage;
        protected string cause;
        public PonyTextException() { }
        public PonyTextException(ProcessingStage stage, string cause)
        {
            this.stage = stage;
            this.cause = cause;
        }
        public override string Message => $"[{stage}] {cause}";
    }
}
