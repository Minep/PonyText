// File: SimpleStringRenderer.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using System.IO;
using System.Text;
using PonyText.Common;
using PonyText.Common.Renderer;

namespace PonyText.Runtime.Renderer {
    public class SimpleStringRenderer : AbstractRendererBase {
        StringBuilder stringBuilder;
        public SimpleStringRenderer() {
            stringBuilder = new StringBuilder();
        }

        public override void EndParagraphRendering() {
            stringBuilder.Append("\r\n");
        }

        public override void RenderContentTo(Stream stream) {
            var sw = new StreamWriter(stream);
            sw.Write(stringBuilder.ToString());
            sw.Flush();
        }

        public override void RenderDirectives(List<string> directives, PropertyConfiguration textProperty) {

        }

        public override void RenderText(string text, PropertyConfiguration textProperty) {
            stringBuilder.Append(text);
        }

        public override void StartParagraphRendering(PropertyConfiguration textProperty) {

        }
    }
}
