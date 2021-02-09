// File: LineBreak.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("br")]
    public class LineBreakProcessor : AbstractProcessor {
        public LineBreakProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            WriteTextLiteral("\r\n");
        }
    }
}
