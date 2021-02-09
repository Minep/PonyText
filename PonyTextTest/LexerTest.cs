using NUnit.Framework;
using PonyText.Common.Parser;
using PonyText.Common.Parser.EventListener;
using PonyText.Parser.Lexer;
using PonyText.Parser.Tokens;
using System;
using System.IO;

namespace PonyTextTest {
    public class LexerTest
    {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void Test1() {
            string docPath = "test/case1.txt";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));

            Assert.IsTrue(res);
        }

        [Test]
        public void Test2() {
            string docPath = "test/case2.txt";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));

            Assert.IsFalse(res);
        }

        [Test]
        public void Test3() {
            string docPath = "test/case3.txt";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));

            Assert.IsFalse(res);
        }

        [Test]
        public void Test4() {
            string docPath = @"E:\桌面工作目录\正在进行的\My Little Pony Novel\无限维度\main.pony";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));

            Assert.IsTrue(res);
        }

        [Test]
        public void Test5() {
            string docPath = @"E:\桌面工作目录\正在进行的\My Little Pony Novel\无限维度\main.pony";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));
            Assert.IsTrue(res);
        }

        [Test]
        public void Test6() {
            string docPath = @"E:\桌面工作目录\正在进行的\Projects\PonyText\README_PonyText.pony";
            PonyLexer lexer = new PonyLexer();
            lexer.onTokenGenerated += Lexer_onTokenGenerated;
            lexer.errorListener = new ErrorListener();
            bool res = lexer.RunGenerator(File.ReadAllText(docPath));
            Assert.IsTrue(res);
        }

        private void Lexer_onTokenGenerated(PonyToken token) {
            Console.WriteLine(token);
        }
    }

    class ErrorListener : IErrorListener
    {
        public void OnLexerErrorReported(string casue, string symbol, int column, int row) {
            Console.WriteLine("[ERROR](Lexer) At character '{1}' on {3}:{2}. {0}", casue, symbol, column, row);
        }

        public void OnParserErrorReported(string casue, int fromState, string token) {
            Console.WriteLine("[ERROR](Parser) At token {0} when performing transition on state {1}. {2}",
                                token, fromState, casue);
        }
    }
}
