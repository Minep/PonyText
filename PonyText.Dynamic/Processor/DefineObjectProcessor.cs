// File: DefineObjectProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Dynamic.PTDRuntime;

namespace PonyText.Dynamic.Processor {

    [Processor("objectPT")]
    public class DefineObjectProcessor : AbstractProcessor {

        PTObjectTable objectTable = PTObjectTable.Instance;
        public DefineObjectProcessor(ProcessorInfo info) : base(info) {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.MapStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string objName = args[0].GetUnderlyingObject() as string;
            objName = $"pt.{objName}";
            PonyTextMapStruct mapStruct = (PonyTextMapStruct)args[1];

            if (objectTable.ContainObject(objName)) {
                throw new PreProcessorException($"Object \"{objName}\" already defined.");
            }

            ExpandoObject obj = new ExpandoObject();
            foreach (var item in mapStruct.GetMappings()) {
                if (!AddObjectMember(obj, item.Key, item.Value)) {
                    throw new PreProcessorException($"Field \"{item.Key}\" already defined.");
                }
            }
            objectTable.TrySetObject(objName, obj);
        }

        private bool AddObjectMember(ExpandoObject obj, string name, PonyTextStructureBase value) {
            string[] parts = name.Split('#');
            if(parts.Length == 1) {
                return obj.TryAdd(name, value);
            }
            switch (parts[0]){
                case "int":
                    decimal? v = value.GetUnderlyingObject() as decimal?;
                    if (v.HasValue) {
                        return obj.TryAdd(parts[1], decimal.ToInt32(v.Value));
                    }
                    throw new PreProcessorException(
                        $"Value with type of {value.StructureType} is not a valid numeric value");
                case "double":
                    decimal? vd = value.GetUnderlyingObject() as decimal?;
                    if (vd.HasValue) {
                        return obj.TryAdd(parts[1], decimal.ToDouble(vd.Value));
                    }
                    throw new PreProcessorException(
                        $"Value with type of {value.StructureType} is not a valid numeric value");
                case "string":
                    string str = value.GetUnderlyingObject() as string;
                    if(str != null) {
                        return obj.TryAdd(parts[1], str);
                    }
                    throw new PreProcessorException(
                        $"Value with type of {value.StructureType} is not a valid literal value");
                default:
                    throw new PreProcessorException(
                        $"Unknown CLR primitive type {parts[0]}.");
            }
        }
    }
}
