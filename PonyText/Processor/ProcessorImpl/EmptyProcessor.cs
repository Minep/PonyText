// File: EmptyProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;

namespace PonyText.Processor.ProcessorImpl {
    public class EmptyProcessor : AbstractProcessor
    {
        public EmptyProcessor() : base(null)
        {
        }

        protected override void OnPreRenderInternal(AbstractTextElement textElement, AbstractRendererBase rendererBase) {
            
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {

        }
    }
}
