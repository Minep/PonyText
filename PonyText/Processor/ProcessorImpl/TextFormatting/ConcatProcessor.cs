// File: ConcatProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("join")]
    public class ConcatProcessor : AbstractProcessor
    {
        public ConcatProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.Any).MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            foreach (var item in args)
            {
                AbstractTextElement element = GetText(item);
                if (element.TextElementType == TextElementType.TextUnit)
                {
                    WriteText(element);
                }
                else
                {
                    foreach (var text in element.UnfoldToAtomic())
                    {
                        WriteText(text);
                    }
                }
            }
        }
    }
}
