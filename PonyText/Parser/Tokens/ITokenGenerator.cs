// File: ITokenGenerator.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Parser;

namespace PonyText.Parser.Tokens
{
    public delegate void TokenGenerated(PonyToken token);
    public interface ITokenGenerator
    {
        event TokenGenerated onTokenGenerated;
        void HaltGenerator();
        bool RunGenerator(string text);

        int GetCurrentRow();
        int GetCurrentColumn();
    }
}
