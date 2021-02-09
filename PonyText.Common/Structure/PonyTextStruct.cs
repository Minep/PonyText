// File: PonyTextStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Parser;

namespace PonyText.Common.Structure {
    public class PonyTextStruct : PonyTextStructureBase {
        List<PonyTextStructureBase> structs;
        public PonyTextStruct(PonyToken token) : base(StructureType.PTextStruct, token) {
            structs = new List<PonyTextStructureBase>();
        }

        public void AddStructure(PonyTextStructureBase textStructureBase) {
            structs.Add(textStructureBase);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            foreach (var item in structs) {
                item.Evaluate(textContext);
            }
        }
    }
}
