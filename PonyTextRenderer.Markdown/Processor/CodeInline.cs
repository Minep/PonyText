// File: CodeInline.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyTextRenderer.Markdown.Processor {
    [Processor("c", AlsoForRender = false)]
    public class CodeInline : AbstractProcessor {
        public CodeInline(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            AbstractTextElement abstractText = GetText(args[0]);
            abstractText.CustomProperty.SetProperty(MarkdownRenderSetting.MD_REGION_INLINE, MarkdownRenderSetting.MD_REGION_CODE);
            WriteText(abstractText);
        }
    }
}
