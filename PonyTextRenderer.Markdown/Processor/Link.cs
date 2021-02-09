// File: Code.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyTextRenderer.Markdown.Processor {
    [Processor("link")]
    public class Link : AbstractProcessor
    {
        public Link(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string link = args[1].GetUnderlyingObject() as string;
            string desc = args[0].GetUnderlyingObject() as string;
            WriteText(ctx.TextElementFactory.CreateTextElement(TextElementType.TextUnit, $"[{desc}]({link})"));
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
