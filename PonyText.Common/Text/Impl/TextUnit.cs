// File: TextUnit.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Text;
using System.Text.RegularExpressions;
using PonyText.Common.Context;
using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Text;

namespace PonyText.Common.Text.Impl {
    public class TextUnit : AbstractTextElement {
        public TextUnit() : base(TextElementType.TextUnit) {

        }
        public string Content {
            get;
        }


        public TextUnit(string content) : this() {
            Content = content;
        }

        public override void Render(AbstractRendererBase rendererBase, PonyTextContext textContext) {
            AbstractProcessor abstractProcessor = null;
            if (CustomProperty.HasProperty(AbstractProcessor.RENDER_PROCESSOR_PROPERTY)) {
                var processor = CustomProperty.GetProperty(AbstractProcessor.RENDER_PROCESSOR_PROPERTY) as string;
                abstractProcessor = textContext.ProcessorFactory.GetRenderProcessor(processor);
                abstractProcessor.PreRendering(textContext, rendererBase, this);
            }
            rendererBase.RenderText(Content, CustomProperty);
            abstractProcessor?.PostRendering(textContext, rendererBase, this);
        }

        public override void AddTextElement(AbstractTextElement textElement) {
            // nothing todo
        }
        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append("{");
            builder.Append(base.ToString());
            builder.Append($",\"Text\":\"{Content.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"}}");
            return builder.ToString();
        }
    }
}
