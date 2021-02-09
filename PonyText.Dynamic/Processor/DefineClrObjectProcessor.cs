// File: DefineClrObjectProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Dynamic.PTDRuntime;

namespace PonyText.Dynamic.Processor {

    [Processor("objectCLR")]
    public class DefineClrObjectProcessor : AbstractProcessor {

        PTObjectTable objectTable = PTObjectTable.Instance;

        public DefineClrObjectProcessor(ProcessorInfo info) : base(info) {
            ProcessorParam.Require(StructureType.LiteralStruct)
                            .Require(StructureType.LiteralStruct)
                            .Require(StructureType.LiteralStruct | StructureType.NumberStruct)
                            .MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            string objName = args[0].GetUnderlyingObject() as string;
            string clrName = args[1].GetUnderlyingObject() as string;
            objName = $"clr.{objName}";

            if (objectTable.ContainObject(objName)) {
                throw new PreProcessorException($"Object named \"{objName}\" already defined.");
            }

            List<object> parameter = new List<object>();
            for (int i = 2; i < args.Length; i++) {
                parameter.Add(args[i].GetUnderlyingObject());
            }
            try {
                object obj = Activator.CreateInstance(null, clrName, parameter.ToArray());
                objectTable.TrySetObject(objName, obj);
            }
            catch(Exception e) {
                throw new PreProcessorException(
                    $"Unable to create instance for CLR object named \"{clrName}\"." +
                    $"Error detail : \"{e.Message}\"");
            }
        }
    }
}
