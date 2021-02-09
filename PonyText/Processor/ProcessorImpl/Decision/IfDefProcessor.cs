// File: IfDefProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.Decision {
    [Processor("ifdef")]
    public class IfDefProcessor : AbstractProcessor {
        public IfDefProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.Any);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string marcoName = args[0].GetUnderlyingObject() as string;
            if (ctx.MacroTable.ContainsMarco(marcoName)) {
                WriteText(GetText(args[1]));
            }
        }
    }
}
