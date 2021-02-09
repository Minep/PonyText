// File: PonyTextMarcoStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Parser;

namespace PonyText.Common.Structure {
    public class PonyTextMarcoStruct : PonyTextStructureBase {
        public string MarcoName { get; }
        private List<PonyTextStructureBase> formatArgs;
        public PonyTextMarcoStruct(PonyToken token) : base(StructureType.MarcoStruct, token) {
            MarcoName = token.Text;
            formatArgs = new List<PonyTextStructureBase>();
        }

        public void AddFormatArgs(PonyTextStructureBase formatArg) {
            formatArgs.Add(formatArg);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            textContext.MacroTable.GetMarco(MarcoName).Evaluate(textContext);
        }
    }
}
