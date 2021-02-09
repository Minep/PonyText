// File: ProcessorFactory.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Reflection;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Utils;
using PonyText.Processor.ProcessorImpl;
using PonyText.Utils;

namespace PonyText.Processor.FactoryImpl {
    public class ProcessorFactory : IProcessorFactory
    {
        private readonly AbstractProcessor emptyProcessor = new EmptyProcessor();
        private Dictionary<string, ProcessorInfo> processors = new Dictionary<string, ProcessorInfo>();
        private AssemblyInjectionManager assemblyInjection;
        public ProcessorFactory(AssemblyInjectionManager assemblyInjection)
        {
            this.assemblyInjection = assemblyInjection;
        }

        public AbstractProcessor GetPreProcessor(string name)
        {
            if (processors.ContainsKey(name))
            {
                return processors[name].processorInstance;
            }
            throw new PonyTextException(ProcessingStage.General, $"Processor '{name}' not found.");
        }

        public AbstractProcessor GetRenderProcessor(string name)
        {
            if (processors.ContainsKey(name))
            {
                ProcessorInfo processor = processors[name];
                if (processor.attribute.AlsoForRender)
                {
                    return processor.processorInstance;
                }
                else
                {
                    return emptyProcessor;
                }
            }
            throw new PonyTextException(ProcessingStage.General, $"Processor '{name}' not found.");
        }

        public void LoadProcessorFrom(string processorNamespace)
        {
            Assembly asm;
            if(!assemblyInjection.TryGetAssembly(processorNamespace, out asm)) {
                throw new PonyTextException(ProcessingStage.General, $"Package '{processorNamespace}' not found");
            }
            processorNamespace += ".Processor";
            Type[] types = asm.GetExportedTypes();
            foreach (var type in types)
            {
                if (type.Namespace != null && type.Namespace.StartsWith(processorNamespace))
                {
                    RegisterProcessor(type);
                }
            }
        }

        public void RegisterProcessor(Type processor)
        {
            ProcessorAttribute attr = Attribute.GetCustomAttribute(processor, typeof(ProcessorAttribute)) as ProcessorAttribute;
            if (attr != null && !processors.ContainsKey(attr.ProcessorName))
            {
                ProcessorInfo info = new ProcessorInfo(attr, processor);
                processors.Add(attr.ProcessorName, info);
            }
        }
    }
}
