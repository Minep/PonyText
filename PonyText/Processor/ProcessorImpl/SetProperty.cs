// File: SetProperty.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl {
    [Processor("set")]
    public class SetProperty : AbstractProcessor
    {
        public SetProperty(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct)
                        .Require(StructureType.LiteralStruct | StructureType.NumberStruct | StructureType.MapStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            if(args[1].StructureType == StructureType.MapStruct) {
                ctx.ContextProperty.SetProperty(args[0].GetUnderlyingObject() as string, CreatePrimitiveMapping(args[1]));
            }
            else {
                ctx.ContextProperty.SetProperty(args[0].GetUnderlyingObject() as string, args[1].GetUnderlyingObject());
            }
        }
    }
}
