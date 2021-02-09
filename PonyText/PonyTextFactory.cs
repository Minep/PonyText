// File: Class1.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.IO;
using System.Text;
using PonyText.Common;
using PonyText.Common.Context;
using PonyText.Common.Structure;
using PonyText.Parser;
using PonyText.Parser.Lexer;

namespace PonyText
{
    public class PonyTextFactory
    {
        public static PonyTextStructureBase CreateEvaluable(string ponyTextContent, string ponyTextIdentifier, PonyTextContext ctx)
        {
            ctx.Metadata.DependencyList.AddDependency(ponyTextIdentifier);

            PonyLexer ponyLexer = new PonyLexer();
            PonyParser parser = new PonyParser(ponyLexer);
            parser.errorListener = GlobalConfiguration.Instance.ParserErrorListener;
            ponyLexer.errorListener = parser.errorListener;

            return parser.Parse(ponyTextContent);
        }

        public static PonyTextStructureBase CreateEvaluable(string ponyTextPath, PonyTextContext ctx) {
            string fileLocation = ctx.Metadata.GetAbsolutePath(ponyTextPath);

            if (!File.Exists(fileLocation))
            {
                throw new ArgumentException($"Unable to process PonyText source file locate at \"{fileLocation}\". Not such file.");
            }

            return CreateEvaluable(File.ReadAllText(fileLocation, Encoding.UTF8), fileLocation, ctx);
        }
    }
}
