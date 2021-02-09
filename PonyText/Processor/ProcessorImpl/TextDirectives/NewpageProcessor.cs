// File: NewpageProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("newpage")]
    public class NewpageProcessor : AbstractProcessor {
        public NewpageProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            WriteDirectives("newpage");
        }
    }
}
