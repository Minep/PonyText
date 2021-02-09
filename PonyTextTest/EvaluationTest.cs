using NUnit.Framework;
using PonyText.Common;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Parser;
using PonyText.Parser.Lexer;
using PonyText.Processor;
using PonyText.Processor.FactoryImpl;
using PonyText.Runtime.Context;
using PonyText.Runtime.Renderer.Impl;
using PonyText.Runtime.Text;
using PonyText.Utils;
using PonyTextRenderer.Markdown;
using PonyTextRenderer.Pdf;
using System.IO;
using System.Reflection;

namespace PonyTextTest
{
    public class EvaluationTestz
    {
        [Test]
        public void TestEvalution() {
            string docPath = @"E:\桌面工作目录\正在进行的\My Little Pony Novel\无限维度\main.pony";
            ErrorListener listener = new ErrorListener();
            GlobalConfiguration globalConfiguration = GlobalConfiguration.Instance;
            PonyLexer lexer = new PonyLexer();
            PonyParser parser = new PonyParser(lexer);
            lexer.errorListener = globalConfiguration.ParserErrorListener;
            parser.errorListener = globalConfiguration.ParserErrorListener;
            PonyTextStructureBase structureBase = parser.Parse(File.ReadAllText(docPath));

            AssemblyInjectionManager injectionManager = new AssemblyInjectionManager();
            injectionManager.RegisterAssembly("PonyTextRenderer.Pdf", Assembly.GetAssembly(typeof(PdfRenderer)));
            injectionManager.RegisterAssembly("PonyText.Processor", Assembly.GetAssembly(typeof(PonyParser)));

            IProcessorFactory processor = new ProcessorFactory(new ProcessorExperience(), injectionManager);
            SimpleTextContext simpleTextContext = new SimpleTextContext(processor, new TextElementFactory());

            processor.LoadProcessorFrom("PonyText.Processor");

            structureBase.Evaluate(simpleTextContext);

            PdfRenderer pdfRenderer = new PdfRenderer(simpleTextContext);
            simpleTextContext.GetCurrentContext().Render(pdfRenderer, simpleTextContext);

            using(FileStream fs = new FileStream("out.pdf", FileMode.Create)) {
                pdfRenderer.RenderContentTo(fs);
            }
        }

        [Test]
        public void TestEvalution2() {
            string docPath = @"E:\桌面工作目录\正在进行的\Projects\PonyText\README_PonyText.pony";
            ErrorListener listener = new ErrorListener();
            PonyLexer lexer = new PonyLexer();
            PonyParser parser = new PonyParser(lexer);

            lexer.errorListener = listener;
            parser.errorListener = listener;

            PonyTextStructureBase structureBase = parser.Parse(File.ReadAllText(docPath));
            AssemblyInjectionManager injectionManager = new AssemblyInjectionManager();

            injectionManager.RegisterAssembly("PonyTextRenderer.Markdown", Assembly.GetAssembly(typeof(MarkdownRenderer)));
            injectionManager.RegisterAssembly("PonyText.Processor", Assembly.GetAssembly(typeof(PonyParser)));

            IProcessorFactory processor = new ProcessorFactory(new ProcessorExperience(), injectionManager);
            SimpleTextContext simpleTextContext = new SimpleTextContext(processor, new TextElementFactory());
            
            processor.LoadProcessorFrom("PonyText.Processor");

            Assert.DoesNotThrow(() => {
                structureBase.Evaluate(simpleTextContext);
            });

            using (FileStream fs = new FileStream("dump.json",FileMode.Create)) {
                using(StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(simpleTextContext.GetCurrentContext().ToString());
                }
            }

            MarkdownRenderer stringRenderer = new MarkdownRenderer();
            simpleTextContext.GetCurrentContext().Render(stringRenderer, simpleTextContext);

            using (FileStream fs = new FileStream("out.md", FileMode.Create)) {
                stringRenderer.RenderContentTo(fs);
            }
        }
    }
}
