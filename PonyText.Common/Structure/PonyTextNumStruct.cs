// File: PonyTextNumStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Context;
using PonyText.Common.Parser;
using PonyText.Common.Text;

namespace PonyText.Common.Structure {
    public class PonyTextNumStruct : PonyTextStructureBase {
        decimal num;
        public PonyTextNumStruct(PonyToken token) : base(StructureType.NumberStruct, token) {
            num = decimal.Parse(token.Text);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            textContext.WriteTextElement(TextElementFactory.CreateTextElement(TextElementType.TextUnit, num.ToString()));
        }

        public override object GetUnderlyingObject() {
            return num;
        }
    }
}
