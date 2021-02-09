using NUnit.Framework;
using PonyText.Common.Structure;
using PonyText.Parser;
using PonyText.Parser.EventListener;
using PonyText.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PonyTextTest
{
    public class ParserTest
    {
        [Test]
        public void TestParser1() {
            string docPath = @"E:\桌面工作目录\正在进行的\My Little Pony Novel\无限维度\chapters\chap1.pony";
            ErrorListener listener = new ErrorListener();
            PonyLexer lexer = new PonyLexer();
            PonyParser parser = new PonyParser(lexer);
            lexer.errorListener = listener;
            parser.errorListener = listener;
            PonyTextStructureBase structureBase = parser.Parse(File.ReadAllText(docPath));
            Assert.IsNotNull(structureBase);
        }

        [Test]
        public void TestParser2() {
            string docPath = @"E:\桌面工作目录\正在进行的\Projects\PonyText\README_PonyText.pony";
            ErrorListener listener = new ErrorListener();
            PonyLexer lexer = new PonyLexer();
            PonyParser parser = new PonyParser(lexer);
            lexer.errorListener = listener;
            parser.errorListener = listener;
            PonyTextStructureBase structureBase = parser.Parse(File.ReadAllText(docPath));
            Assert.IsNotNull(structureBase);
        }
    }
}
