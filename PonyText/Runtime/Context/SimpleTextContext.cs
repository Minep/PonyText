// File: SimpleTextContext.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Context;
using PonyText.Common.Processor;
using PonyText.Common.Text.Impl;

namespace PonyText.Runtime.Context
{
    public class SimpleTextContext : PonyTextContext
    {
        public SimpleTextContext(IProcessorFactory processorFactory)
            : base(new TextElementCollection(), processorFactory)
        {

        }
    }
}
