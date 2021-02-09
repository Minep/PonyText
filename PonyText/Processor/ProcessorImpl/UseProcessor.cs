// File: UseProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl {
    [Processor("use")]
    public class UseProcessor : AbstractProcessor
    {
        public UseProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string nsapce = args[0].GetUnderlyingObject() as string;
            ctx.ProcessorFactory.LoadProcessorFrom(nsapce);
        }
    }
}
