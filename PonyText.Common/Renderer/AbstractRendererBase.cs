// File: AbstractRendererBase.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using System.IO;

namespace PonyText.Common.Renderer
{
    public abstract class AbstractRendererBase
    {
        public RendererPropertyChain PropertyChain { get; }

        public AbstractRendererBase()
        {
            PropertyChain = new RendererPropertyChain();
        }

        public virtual void EnterRenderContext(PropertyConfiguration contextProperty)
        {
            PropertyChain.PushPropertyContext(contextProperty);
        }
        public virtual void LeaveRenderContext()
        {
            PropertyChain.PopPropertyContext();
        }

        
        public abstract void RenderText(string text, PropertyConfiguration textProperty);
        public abstract void RenderDirectives(List<string> directives, PropertyConfiguration textProperty);
        public virtual void StartParagraphRendering(PropertyConfiguration textProperty)
        {
            EnterRenderContext(textProperty);
        }
        public virtual void EndParagraphRendering()
        {
            LeaveRenderContext();
        }
        public abstract void RenderContentTo(Stream stream);
    }
}
