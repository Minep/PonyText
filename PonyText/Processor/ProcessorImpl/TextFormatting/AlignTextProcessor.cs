// File: AlignTextProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("align")]
    public class AlignTextProcessor : AbstractProcessor
    {
        public AlignTextProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.Any)
                        .Require(StructureType.LiteralStruct)
                        .RequireOptional(StructureType.LiteralStruct)
                        .RequireOptional(StructureType.MapStruct);
        }
    
        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            AbstractTextElement abstractText = GetText(args[0]);
            string halignDirection = args[1].GetUnderlyingObject() as string;
            abstractText.CustomProperty.SetProperty(RenderSettingField.HORIZONTAL_ALIGN, halignDirection);
            if (args.Length >= 3)
            {
                string valignDirection = args[2].GetUnderlyingObject() as string;
                abstractText.CustomProperty.SetProperty(RenderSettingField.VERTICAL_ALIGN, valignDirection);
            }
            if(args.Length == 4){
                abstractText.CustomProperty.Merge(CreatePrimitiveMapping(args[3]));
            }
            WriteText(abstractText);
        }
    }
}
