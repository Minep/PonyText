// File: PonyTextMapStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Exceptions;
using PonyText.Common.Parser;

namespace PonyText.Common.Structure {
    public class PonyTextMapStruct : PonyTextStructureBase {
        Dictionary<string, PonyTextStructureBase> map;

        public PonyTextMapStruct(PonyToken token) : base(StructureType.MapStruct, token) {
            map = new Dictionary<string, PonyTextStructureBase>();
        }

        public void AddToMap(string key, PonyTextStructureBase structureBase) {
            if (!map.ContainsKey(key)) {
                map.Add(key, structureBase);
            }
            else {
                throw new PonyTextException(ProcessingStage.PreProcessing, $"Key '{key}' already exist");
            }
        }

        public Dictionary<string, PonyTextStructureBase> GetMappings() {
            return map;
        }

        public override void EvaluateInternal(PonyTextContext textContext) {

        }
    }
}
