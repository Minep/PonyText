// File: HeadingProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Utils;
using PonyText.Processor.Misc;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("heading")]
    public class HeadingProcessor : AbstractProcessor
    {
        HeaderCounter headerCounter;
        public HeadingProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            headerCounter = HeaderCounter.Instance;
            ProcessorParam.Require(StructureType.NumberStruct).RequireOptional(StructureType.Any);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            int level = decimal.ToInt32((decimal)args[0].GetUnderlyingObject());
            ctx.GetCurrentContext().CustomProperty.SetProperty(RenderSettingField.HEADING_LEVEL, level);
            if (headerCounter.EnableNumbering) {
                string prefix = headerCounter.getHeadingNumbering(level - 1);
                headerCounter.Count(level - 1);
                WriteTextLiteral(prefix);
            }
            if(args.Length>1){
                args[1].Evaluate(ctx);
            }
            //WriteText(textElement);
        }
    }
}
