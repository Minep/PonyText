// File: IncludeText.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.IO;
using PonyText.Common;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Utils;
using PonyText.Parser;
using PonyText.Parser.Lexer;

namespace PonyText.Processor.ProcessorImpl {
    [Processor("include")]
    public class IncludeText : AbstractProcessor
    {
        public IncludeText(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string fileLocation = args[0].GetUnderlyingObject() as string;
                
            PonyTextStructureBase structureBase = PonyTextFactory.CreateEvaluable(fileLocation, ctx);

            try {
                structureBase.Evaluate(ctx);
                logger.LogInfo($"Pony file: '{fileLocation}' imported successfully.");
            }
            catch(PreProcessorException e) {
                logger.LogError($"Fail on importing pony file: '{fileLocation}'.");
                e.AddToStackTrace(PreProcessTrace.CreateTraceFileNameLevel(fileLocation));
                throw e;
            }
        }
    }
}
