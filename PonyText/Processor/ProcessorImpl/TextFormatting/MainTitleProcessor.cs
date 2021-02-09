// File: MainTitleProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl {
    [Processor("maintitle")]
    public class MainTitleProcessor : AbstractProcessor
    {
        public MainTitleProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .RequireOptional(StructureType.MapStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string title = args[0].GetUnderlyingObject() as string;
            AbstractTextElement text = TextElementFactory.CreateTextElement(TextElementType.Paragraph, title);
            text.CustomProperty.SetProperty(RenderSettingField.HORIZONTAL_ALIGN, RenderSettingField.ALIGN_VH_MID);
            text.CustomProperty.SetProperty(RenderSettingField.VERTICAL_ALIGN, RenderSettingField.ALIGN_VH_MID);
            text.CustomProperty.SetProperty(RenderSettingField.TEXT_FONTSIZE, "26");
            if (args.Length == 2)
            {
                text.CustomProperty.Merge(CreatePrimitiveMapping(args[1]));
            }
            WriteText(text);
        }
    }
}
