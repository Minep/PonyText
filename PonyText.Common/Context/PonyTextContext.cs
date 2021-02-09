// File: PonyTextContext.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Processor;
using PonyText.Common.Text;
using PonyText.Runtime.Common;

namespace PonyText.Common.Context
{
    public abstract class PonyTextContext
    {
        protected AbstractTextElement textElement;

        public virtual PropertyConfiguration ContextProperty { get; }
        public virtual IProcessorFactory ProcessorFactory { get; }
        public virtual ContextMetadata Metadata { get; }
        public virtual MacroTable MacroTable
        {
            get;
        }

        protected PonyTextContext()
        {

        }

        protected PonyTextContext(AbstractTextElement textElement, IProcessorFactory processorFactory)
        {
            this.textElement = textElement;
            ProcessorFactory = processorFactory;
            MacroTable = new MacroTable();
            ContextProperty = new PropertyConfiguration();
            Metadata = new ContextMetadata();
        }

        public virtual void WriteTextElement(AbstractTextElement text)
        {
            textElement.AddTextElement(text);
        }

        public virtual AbstractTextElement GetCurrentContext()
        {
            return textElement;
        }


    }
}
