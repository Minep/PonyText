// File: DefineMarco.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl {
    [Processor("define")]
    public class DefineMarco : AbstractProcessor
    {
        public DefineMarco(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct)
                        .Require(StructureType.Any);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string name = args[0].GetUnderlyingObject() as string;
            ctx.MacroTable.PutMarco(name, args[1]);
        }
    }
}
