// File: DiretiveInjectProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("directives")]
    public class DiretiveInjectProcessor : AbstractProcessor {
        public DiretiveInjectProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.RequireOptional(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string[] argstrs = args
                                .Select(x => x.GetUnderlyingObject() as string)
                                .ToArray();
            WriteDirectives(argstrs);
        }
    }
}
