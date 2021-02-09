// File: IProcessorFactory.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;

namespace PonyText.Common.Processor {
    public interface IProcessorFactory
    {
        void LoadProcessorFrom(string processorNamespace);
        AbstractProcessor GetRenderProcessor(string name);
        void RegisterProcessor(Type processor);
        AbstractProcessor GetPreProcessor(string name);
    }
}
