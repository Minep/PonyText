using PonyText.Common.Processor;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("endseries")]
    public class EndSeriesProcessor : AbstractProcessor {
        public EndSeriesProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.RequireOptional(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            WriteDirectives("endseries");
        }
    }
}
