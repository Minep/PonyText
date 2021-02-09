// File: ProcessorInfo.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using PonyText.Common.Utils;

namespace PonyText.Common.Processor {
    public class ProcessorInfo
    {
        private AbstractProcessor instanceHolder;
        public ProcessorAttribute attribute { get; }
        public Type processorType { get; }
        public AbstractProcessor processorInstance
        {
            get
            {
                return (AbstractProcessor)(instanceHolder ?? Activator.CreateInstance(processorType, this));
            }
        }

        public ProcessorInfo(ProcessorAttribute attribute, Type processorType)
        {
            this.attribute = attribute;
            this.processorType = processorType;
            if (attribute.Singleton)
            {
                instanceHolder = (AbstractProcessor)Activator.CreateInstance(processorType, this);
            }
        }
    }
}
