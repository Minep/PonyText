// File: TextDirectives.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Context;
using PonyText.Common.Renderer;
using PonyText.Common.Text;

namespace PonyText.Common.Text.Impl {
    public class TextDirectives : AbstractTextElement {

        List<string> directives;
        public TextDirectives() : base(TextElementType.TextDirective) {
            directives = new List<string>();
        }

        public void AddDirectives(string[] strs) {
            directives.AddRange(strs);
        }

        public override void AddTextElement(AbstractTextElement textElement) {
            throw new NotImplementedException();
        }

        public override void Render(AbstractRendererBase rendererBase, PonyTextContext textContext) {
            rendererBase.RenderDirectives(directives, CustomProperty);
        }
    }
}
