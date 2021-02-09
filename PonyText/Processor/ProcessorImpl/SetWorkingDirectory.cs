using System;
using System.IO;
using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl {

    [Processor("workingDirectory")]
    public class SetWorkingDirectory : AbstractProcessor {
        public SetWorkingDirectory(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string dir = args[0].GetUnderlyingObject() as string;
            if(Directory.Exists(dir) && Path.IsPathRooted(dir)){
                ctx.Metadata.WorkingDirectory = dir;
            }
            else {
                throw new ArgumentException($"Working directory path must be existed and non-relative");
            }
        }
    }
}