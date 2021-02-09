using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;

namespace PonyText.Processor.ProcessorImpl.TextDirectives {
    [Processor("stySeries")]
    public class BoldSeriesProcessor : AbstractProcessor {
        public BoldSeriesProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string style = args[0].GetUnderlyingObject() as string;
            switch(style){
                case RenderSettingField.TEXT_STYLE_BOLD:
                    WriteDirectives("btseries");
                    break;
                case RenderSettingField.TEXT_STYLE_ITALIC:
                    WriteDirectives("tiseries");
                    break;
                case RenderSettingField.TEXT_STYLE_UNDERLINE:
                    WriteDirectives("tuseries");
                    break;
                case RenderSettingField.TEXT_STYLE_DELETELINE:
                    WriteDirectives("tdseries");
                    break;
            }
        }
    }
}
