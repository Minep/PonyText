// File: ProcessorParamConfig.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using PonyText.Common.Exceptions;
using PonyText.Common.Structure;

namespace PonyText.Common.Utils
{
    public class ProcessorParamConfig
    {
        struct ParamType
        {
            public StructureType type;
            public bool isOptional;

            public ParamType(StructureType type, bool isOptional)
            {
                this.type = type;
                this.isOptional = isOptional;
            }
        }

        bool isParametersFixed = true;
        int optionalCount = 0;

        List<ParamType> paramTypes = new List<ParamType>();

        public static ProcessorParamConfig CreateParamsConfig()
        {
            return new ProcessorParamConfig();
        }

        public ProcessorParamConfig Require(StructureType type)
        {
            paramTypes.Add(new ParamType(type, false));
            return this;
        }

        public ProcessorParamConfig RequireOptional(StructureType type)
        {
            paramTypes.Add(new ParamType(type, true));
            optionalCount++;
            return this;
        }

        public ProcessorParamConfig MakeVariable()
        {
            if(paramTypes.Count == 0) {
                throw new PreProcessorException("Variable parameter list must be at least one parameter type");
            }
            isParametersFixed = false;
            return this;
        }

        public void ValidateParameter(PonyTextStructureBase[] structureBases)
        {
            if (isParametersFixed) {
                if (paramTypes.Count < structureBases.Length) {
                    throw new PreProcessorException($"Too many parameter. Require at most {paramTypes.Count} parameters but {structureBases.Length} found");
                }
                else if (paramTypes.Count - structureBases.Length > optionalCount) {
                    throw new PreProcessorException($"Too few parameter. Require at least {paramTypes.Count - optionalCount} parameters but {structureBases.Length} found");
                }
                if(paramTypes.Count == 0 && structureBases.Length == 0){
                    return;
                }
            }
            int count = 0;
            ParamType last = paramTypes[paramTypes.Count - 1];
            while (count < structureBases.Length) {
                ParamType Paratype;
                if(count < paramTypes.Count) {
                    Paratype = paramTypes[count];
                }
                else {
                    Paratype = last;
                }
                if (!Paratype.type.HasFlag(structureBases[count].StructureType)) {
                    throw new PreProcessorException(
                        $"At {count + 1} th parameter. Require {Paratype.type} but {structureBases[count].StructureType} found.");
                }
                count++;
            }
        }
    }
}
