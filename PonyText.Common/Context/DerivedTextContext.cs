// File: DerivedTextContext.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Text;
using PonyText.Runtime.Common;

namespace PonyText.Common.Context
{
    public class DerivedTextContext : PonyTextContext
    {
        PonyTextContext origin;

        public override PropertyConfiguration ContextProperty => origin.ContextProperty;
        public override ContextMetadata Metadata => origin.Metadata;
        public override MacroTable MacroTable => origin.MacroTable;
        public override IProcessorFactory ProcessorFactory => origin.ProcessorFactory;
        public DerivedTextContext(PonyTextContext origin, AbstractTextElement newConext) : base()
        {
            this.origin = origin;
            textElement = newConext;
        }
    }
}
