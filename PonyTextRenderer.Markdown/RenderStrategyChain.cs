// File: RenderingStageChain.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyTextRenderer.Markdown.RenderStrategy;

namespace PonyTextRenderer.Markdown
{
    public class RenderStrategyChain
    {
        List<StrategyBase> strategies = new List<StrategyBase>();
        int currentPos = 0;

        public RenderStrategyChain() {
            strategies.Add(new HeaderStrategy());
            strategies.Add(new TextStyleStrategy());
        }

        public void TryRenderFromStart(string text, MarkdownRenderer renderer)
        {
            currentPos = 0;
            TryNextStrategy(text, renderer);
        }

        public void TryNextStrategy(string text, MarkdownRenderer renderer)
        {
            if (currentPos < strategies.Count)
            {
                strategies[currentPos++].TryRender(text, renderer, this);
            }
            else
            {
                //if no strategy satisfy.
                renderer.RenderOut.Append(text);
            }
        }
    }
}
