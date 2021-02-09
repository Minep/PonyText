// File: GetMemberProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Dynamic.PTDRuntime;

namespace PonyText.Dynamic.Processor {

    [Processor("member")]
    public class GetMemberProcessor : AbstractProcessor {
        PTObjectTable objectTable = PTObjectTable.Instance;
        public GetMemberProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string objName = args[0].GetUnderlyingObject() as string;
            string memName = args[1].GetUnderlyingObject() as string;
            object dObj;
            if(!objectTable.TryGetObject(objName, out dObj)) {
                throw new PreProcessorException($"Object \"{objName}\" not defined.");
            }

            if(dObj is IDictionary<string, object>) {
                IDictionary<string, object> dic = (IDictionary<string, object>)dObj;
                object val;
                if(!dic.TryGetValue(memName, out val)) {
                    throw new PreProcessorException($"Field \"{memName}\" not found in object named \"{objName}\"");
                }
                WriteText(WrapAsTextElement(val.GetType(), val));
            }
            else {
                FieldInfo info = dObj.GetType().GetField(memName);
                if (info == null) {
                    throw new PreProcessorException($"Field \"{memName}\" not found in object named \"{objName}\"");
                }
                WriteText(WrapAsTextElement(info.FieldType, info.GetValue(dObj)));
            }
        }

        private AbstractTextElement WrapAsTextElement(Type type, object value) {
            if (typeof(PonyTextStructureBase).IsAssignableFrom(type)) {
                return GetText(value as PonyTextStructureBase);
            }
            TypeCode tcode = Type.GetTypeCode(type);
            switch (tcode) {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Char:
                case TypeCode.String:
                    return TextElementFactory.CreateTextElement(TextElementType.TextUnit, value.ToString());
                default:
                    throw new PreProcessorException(
                        $"Unable to find a suitable translation from {tcode} ({type.FullName})" +
                        $"to AbstractTextElement");
            }
        }
    }
}
