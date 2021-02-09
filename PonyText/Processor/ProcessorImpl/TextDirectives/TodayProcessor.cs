// File: TodayProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Text.Impl;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("today")]
    public class TodayProcessor : AbstractProcessor {
        public TodayProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.RequireOptional(StructureType.LiteralStruct | StructureType.MarcoStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string format = "D";
            if (args.Length == 1) {
                AbstractTextElement abstractTextElement = GetText(args[0]);
                if (abstractTextElement.TextElementType == TextElementType.TextUnit) {
                    format = (abstractTextElement as TextUnit).Content;
                }
            }
            WriteTextLiteral(DateTime.Now.ToString(format));
        }
    }
}
