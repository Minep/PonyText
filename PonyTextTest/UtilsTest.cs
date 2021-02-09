using NUnit.Framework;
using PonyText.Common.Exceptions;
using PonyText.Common.Structure;
using PonyText.Common.Utils;
using PonyText.Runtime.Structure;
using PonyText.Utils.Verbolization;
using System;

namespace PonyTextTest {
    public class UtilsTest
    {
        [Test]
        public void TestParamConfig() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct);
            paramConfig.Require(StructureType.NumberStruct);
            paramConfig.Require(StructureType.NumberStruct);

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("4")
            };
            Assert.DoesNotThrow(() => paramConfig.ValidateParameter(structureBases));
        }

        [Test]
        public void TestParamConfigOptional() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct);
            paramConfig.Require(StructureType.NumberStruct);
            paramConfig.RequireOptional(StructureType.NumberStruct);

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc"),
                new PonyTextNumStruct("2"),
            };
            Assert.DoesNotThrow(() => paramConfig.ValidateParameter(structureBases));
        }

        [Test]
        public void TestParamConfigVariabe() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct)
                        .Require(StructureType.NumberStruct).MakeVariable();

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2")
            };
            Assert.DoesNotThrow(() => paramConfig.ValidateParameter(structureBases));
        }

        [Test]
        public void TestParamConfigFailedOnType() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct)
                        .Require(StructureType.MarcoStruct);

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc"),
                new PonyTextNumStruct("2")
            };
            Exception e = Assert.Throws(typeof(PreProcessorException), () => paramConfig.ValidateParameter(structureBases));
            Console.WriteLine(e.Message);
        }

        [Test]
        public void TestParamConfigFailedOnFew() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct)
                        .Require(StructureType.MarcoStruct)
                        .RequireOptional(StructureType.MarcoStruct);

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc")
            };
            Exception e = Assert.Throws(typeof(PreProcessorException), () => paramConfig.ValidateParameter(structureBases));
            Console.WriteLine(e.Message);
        }

        [Test]
        public void TestParamConfigFailedMore() {
            ProcessorParamConfig paramConfig = new ProcessorParamConfig();
            paramConfig.Require(StructureType.ParagraphStruct)
                        .Require(StructureType.MarcoStruct)
                        .Require(StructureType.MarcoStruct);

            PonyTextStructureBase[] structureBases = new PonyTextStructureBase[]
            {
                new PonyTextParagraphStruct("abc"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2"),
                new PonyTextNumStruct("2"),
            };
            Exception e = Assert.Throws(typeof(PreProcessorException), () => paramConfig.ValidateParameter(structureBases));
            Console.WriteLine(e.Message);
        }

        [Test]
        public void TestVerbalize() {
            ChineseVerbalizer chineseVerbalizer = new ChineseVerbalizer();
            Assert.DoesNotThrow(() => {
                Console.WriteLine(chineseVerbalizer.verbalize(120345));
                Console.WriteLine(chineseVerbalizer.verbalize(200001));
                Console.WriteLine(chineseVerbalizer.verbalize(01));
                Console.WriteLine(chineseVerbalizer.verbalize(214));
                Console.WriteLine(chineseVerbalizer.verbalize(0));
                Console.WriteLine(chineseVerbalizer.verbalize(int.MaxValue));
                Console.WriteLine(chineseVerbalizer.verbalize(int.MinValue));
                Console.WriteLine(chineseVerbalizer.verbalize(232.2234));
                Console.WriteLine(chineseVerbalizer.verbalize(-2.034));
                Console.WriteLine(chineseVerbalizer.verbalize(5.00));
                Console.WriteLine(chineseVerbalizer.verbalize(0.231));
                Console.WriteLine(chineseVerbalizer.verbalizeInteger("1234324551124560000342908"));
            });
            Assert.Throws(typeof(ArgumentException), () => {
                Console.WriteLine(chineseVerbalizer.verbalizeInteger("12343245511245675454232577654323231"));
            });
        }
    }
}
