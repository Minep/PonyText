// File: TextStyleStrategy.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Renderer;

namespace PonyTextRenderer.Markdown.RenderStrategy {
    public class TextStyleStrategy : StrategyBase {
        public override void TryRender(string text, MarkdownRenderer renderer, RenderStrategyChain strategyChain) {
            string type;
            if(renderer.PropertyChain.TryGetProperty(RenderSettingField.TEXT_STYLE, out type)) {
                switch (type) {
                    case RenderSettingField.TEXT_STYLE_BOLD:
                        renderer.RenderOut.Append($" **{text}** ");
                        break;
                    case RenderSettingField.TEXT_STYLE_ITALIC:
                        renderer.RenderOut.Append($" *{text}* ");
                        break;
                    case RenderSettingField.TEXT_STYLE_DELETELINE:
                        renderer.RenderOut.Append($" ~~{text}~~ ");
                        break;
                    default:
                        renderer.RenderOut.Append(text);
                        break;
                }
                return;
            }
            else if (renderer.PropertyChain.TryGetProperty(MarkdownRenderSetting.MD_REGION_INLINE, out type)) {
                switch (type) {
                    case MarkdownRenderSetting.MD_REGION_CODE:
                        renderer.RenderOut.Append($" `{text}` ");
                        break;
                }
                return;
            }
            strategyChain.TryNextStrategy(text, renderer);
        }
    }
}
