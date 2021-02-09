// File: MarkdownRenderer.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PonyText.Common;
using PonyText.Common.Renderer;

namespace PonyTextRenderer.Markdown {
    [Renderer("Markdown")]
    public class MarkdownRenderer : AbstractRendererBase {
        public StringBuilder RenderOut { get; }
        RenderStrategyChain renderStrategyChain;

        public MarkdownRenderer() {
            RenderOut = new StringBuilder();
            renderStrategyChain = new RenderStrategyChain();
        }

        public override void RenderContentTo(Stream stream) {
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(RenderOut.ToString());
            sw.Flush();
        }

        public override void StartParagraphRendering(PropertyConfiguration textProperty) {
            string opt;
            if(PropertyChain.CurrentContextConfiguration != null &&
                PropertyChain.CurrentContextConfiguration.TryGetProperty(MarkdownRenderSetting.MD_REGION,out opt)){
                if (opt == MarkdownRenderSetting.MD_REGION_LIST) {
                    RenderOut.Append("+ ");
                }
                else if (opt == MarkdownRenderSetting.MD_REGION_LIST_ORDER) {
                    RenderOut.Append("1. ");
                }
                else if (opt == MarkdownRenderSetting.MD_REGION_LIST_CB) {
                    RenderOut.Append("- [ ] ");
                }
                else if (opt == MarkdownRenderSetting.MD_REGION_QUOTE) {
                    RenderOut.Append("> ");
                }
            }
            base.StartParagraphRendering(textProperty);
        }

        public override void RenderText(string text, PropertyConfiguration textProperty) {
            if(textProperty == null) {
                RenderOut.Append(text);
            }
            else {
                EnterRenderContext(textProperty);
                renderStrategyChain.TryRenderFromStart(text, this);
                LeaveRenderContext();
            }
        }

        public override void EndParagraphRendering() {
            base.EndParagraphRendering();
            string opt;
            if (PropertyChain.CurrentContextConfiguration != null &&
               PropertyChain.CurrentContextConfiguration.TryGetProperty(MarkdownRenderSetting.MD_REGION, out opt) &&
               opt==MarkdownRenderSetting.MD_REGION_CODE) {
                RenderOut.Append("\r\n");
            }
            else {
                RenderOut.Append("\r\n\r\n");
            }
        }

        public override void RenderDirectives(List<string> directives, PropertyConfiguration textProperty) {
            throw new NotImplementedException();
        }
    }
}
