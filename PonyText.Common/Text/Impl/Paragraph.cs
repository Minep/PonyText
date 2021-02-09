// File: Paragraph.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Context;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Text;
using PonyText.Common.Utils;

namespace PonyText.Common.Text.Impl {
    public class Paragraph : AbstractTextElement, IEnumerable<AbstractTextElement> {
        List<AbstractTextElement> texts;
        public Paragraph() : base(TextElementType.Paragraph) {
            texts = new List<AbstractTextElement>();
        }

        public override void AddTextElement(AbstractTextElement text) {
            if (text.TextElementType == TextElementType.TextUnit ||
                text.TextElementType == TextElementType.TextDirective) {
                texts.Add(text);
            }
            else {
                //TODO
                //Perhaps, consider throw exception?
                //Or unfold Paragraph/TextElementCollection into Text? (Time complexity!)
                //throw new PonyTextException(ProcessingStage.Rendering, "Paragraph can only be a collection of TextUnit or TextDirectives but not others.");

                texts.AddRange(text.UnfoldToAtomic());
            }
        }

        public IEnumerator<AbstractTextElement> GetEnumerator() {
            return texts.GetEnumerator();
        }

        public override void Render(AbstractRendererBase rendererBase, PonyTextContext textContext) {
            rendererBase.StartParagraphRendering(CustomProperty);
            AbstractProcessor abstractProcessor = null;
            if (CustomProperty.HasProperty(AbstractProcessor.RENDER_PROCESSOR_PROPERTY)) {
                var processor = CustomProperty.GetProperty(AbstractProcessor.RENDER_PROCESSOR_PROPERTY) as string;
                abstractProcessor = textContext.ProcessorFactory.GetRenderProcessor(processor);
                abstractProcessor.PreRendering(textContext, rendererBase, this);
            }
            foreach (var text in texts) {
                text.Render(rendererBase, textContext);
            }
            abstractProcessor?.PostRendering(textContext, rendererBase, this);
            rendererBase.EndParagraphRendering();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return texts.GetEnumerator();
        }

        public override string ToString() {
            var strs = new List<string>();
            foreach (var item in texts) {
                strs.Add(item.ToString());
            }
            var sb = new StringBuilder();
            sb.Append($"{{{base.ToString()},");
            sb.Append($"\"Texts\": [");
            sb.AppendJoin(',', strs);
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
