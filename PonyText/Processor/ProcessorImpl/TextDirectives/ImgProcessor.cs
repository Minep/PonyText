// File: ImgProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {

    [Processor("img")]
    public class ImgProcessor : AbstractProcessor {
        public ImgProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .RequireOptional(StructureType.MapStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            AbstractTextElement abstractText = 
                    TextElementFactory.CreateTextElement(TextElementType.TextUnit);
            PropertyConfiguration configuration = abstractText.CustomProperty;
            if (args.Length == 2) {
                configuration.Merge(CreatePrimitiveMapping(args[1]));
            }
            string path = ctx.Metadata.GetAbsolutePath(args[0].GetUnderlyingObject() as string);
            configuration.SetProperty("utype", "img");
            configuration.SetProperty("path", path);
            WriteText(abstractText);
        }
    }
}
