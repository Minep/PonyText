// File: TextStyleBoldProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("b")]
    public class TextStyleBoldProcessor : AbstractProcessor {
        public TextStyleBoldProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct| StructureType.DirectiveStruct)
                            .MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            foreach (var item in args)
            {
                AbstractTextElement textElement = GetText(item);
                textElement.CustomProperty.AppendProperty(RenderSettingField.TEXT_STYLE, RenderSettingField.TEXT_STYLE_BOLD);
                WriteText(textElement);
            }
        }
    }

    [Processor("i")]
    public class TextStyleItalicProcessor : AbstractProcessor {
        public TextStyleItalicProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct | StructureType.DirectiveStruct)
                            .MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            foreach (var item in args)
            {
                AbstractTextElement textElement = GetText(item);
                textElement.CustomProperty.AppendProperty(RenderSettingField.TEXT_STYLE, RenderSettingField.TEXT_STYLE_ITALIC);
                WriteText(textElement);
            }
        }
    }

    [Processor("u")]
    public class TextStyleUnderlineProcessor : AbstractProcessor {
        public TextStyleUnderlineProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct | StructureType.DirectiveStruct)
                            .MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            foreach (var item in args)
            {
                AbstractTextElement textElement = GetText(item);
                textElement.CustomProperty.AppendProperty(RenderSettingField.TEXT_STYLE, RenderSettingField.TEXT_STYLE_UNDERLINE);
                WriteText(textElement);
            }
        }
    }

    [Processor("d")]
    public class TextStyleDeleteLineProcessor : AbstractProcessor {
        public TextStyleDeleteLineProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct | StructureType.DirectiveStruct).MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            foreach (var item in args)
            {
                AbstractTextElement textElement = GetText(item);
                textElement.CustomProperty.AppendProperty(RenderSettingField.TEXT_STYLE, RenderSettingField.TEXT_STYLE_DELETELINE);
                WriteText(textElement);
            }
        }
    }

    [Processor("style")]
    public class TextStyleChangeStyleProcessor : AbstractProcessor {
        public TextStyleChangeStyleProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.MapStruct)
                            .Require(StructureType.LiteralStruct | StructureType.DirectiveStruct)
                            .MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            Dictionary<string, object> style = CreatePrimitiveMapping(args[0]);
            for (int i = 1; i < args.Length; i++) {
                var item = args[i];
                AbstractTextElement textElement = GetText(item);
                textElement.CustomProperty.Merge(style);
                WriteText(textElement);
            }
        }
    }
}
