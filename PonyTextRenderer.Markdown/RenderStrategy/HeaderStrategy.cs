// File: HeaderRender.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Renderer;

namespace PonyTextRenderer.Markdown.RenderStrategy
{
    public class HeaderStrategy : StrategyBase
    {
        int[] numbering = new[] { 0, 0, 0, 0 };
        public override void TryRender(string text, MarkdownRenderer renderer, RenderStrategyChain strategyChain)
        {
            int level;
            if(renderer.PropertyChain.TryGetProperty(RenderSettingField.HEADING_LEVEL, out level))
            {
                if (1 <= level && level <= 4) {
                    increament(level);

                    string lab = "";
                    for (int i = 0; i < level; i++) {
                        renderer.RenderOut.Append("#");
                        lab += $"{numbering[i]}.";
                    }
                    renderer.RenderOut.Append($" {lab} {text}");
                    return;
                }
            }
            strategyChain.TryNextStrategy(text, renderer);
        }

        private void increament(int index) {
            numbering[index - 1] += 1;
            for (; index < numbering.Length; index++) {
                numbering[index] = 0;
            }
        }
    }
}
