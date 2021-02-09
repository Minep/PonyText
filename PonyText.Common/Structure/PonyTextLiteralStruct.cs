// File: PonyTextLiteralStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Context;
using PonyText.Common.Parser;
using PonyText.Common.Text;

namespace PonyText.Common.Structure {
    public class PonyTextLiteralStruct : PonyTextStructureBase {
        string content;
        public PonyTextLiteralStruct(PonyToken token) : base(StructureType.LiteralStruct, token) {
            content = token.Text.Trim('"');
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            textContext.WriteTextElement(TextElementFactory.CreateTextElement(TextElementType.TextUnit, content));
        }

        public override object GetUnderlyingObject() {
            return content;
        }
    }
}
