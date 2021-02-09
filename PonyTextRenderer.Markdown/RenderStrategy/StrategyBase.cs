// File: StrategyBase.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Renderer;

namespace PonyTextRenderer.Markdown.RenderStrategy
{
    public abstract class StrategyBase
    {
        public abstract void TryRender(string text, MarkdownRenderer renderer, RenderStrategyChain strategyChain);
    }
}
