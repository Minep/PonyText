// File: Code.cs 
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
    [Processor("region", AlsoForRender = true)]
    public class Region : AbstractProcessor
    {
        public Region(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.PTextStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            AbstractTextElement abstractTextElement = GetText(args[1]);
            string type = args[0].GetUnderlyingObject() as string;
            if(abstractTextElement.TextElementType == TextElementType.TextUnit)
            {
                AbstractTextElement paragraph = ctx.TextElementFactory.CreateTextElement(TextElementType.Paragraph);
                paragraph.AddTextElement(abstractTextElement);
                abstractTextElement = paragraph;
            }
            abstractTextElement.CustomProperty.SetProperty(MarkdownRenderSetting.MD_REGION, type);
            WriteText(abstractTextElement, true);
        }

        protected override void OnPreRenderInternal(AbstractTextElement textElement, AbstractRendererBase rendererBase) {
            string opt;
            if (rendererBase.PropertyChain.TryGetProperty(MarkdownRenderSetting.MD_REGION, out opt)) {
                if (opt == MarkdownRenderSetting.MD_REGION_CODE) {
                    rendererBase.RenderText("```\r\n", null);
                }
            }
        }

        protected override void OnPostRenderInternal(AbstractTextElement textElement, AbstractRendererBase rendererBase) {
            string opt;
            if (rendererBase.PropertyChain.TryGetProperty(MarkdownRenderSetting.MD_REGION, out opt)) {
                if (opt == MarkdownRenderSetting.MD_REGION_CODE) {
                    rendererBase.RenderText("```\r\n", null);
                }
            }
        }
    }
}
