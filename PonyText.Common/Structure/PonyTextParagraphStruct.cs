// File: PonyTextParagraphStruct.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Parser;
using PonyText.Common.Text;

namespace PonyText.Common.Structure {
    public class PonyTextParagraphStruct : PonyTextStructureBase {
        List<PonyTextStructureBase> structureBases;
        public PonyTextParagraphStruct(PonyToken token) : base(StructureType.ParagraphStruct, token) {
            structureBases = new List<PonyTextStructureBase>();

            if (!string.IsNullOrEmpty(token.Text)) {
                structureBases.Add(new PonyTextLiteralStruct(token));
            }
        }

        public void AddToParagraph(PonyTextStructureBase structureBase) {
            structureBases.Add(structureBase);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            var textElement = TextElementFactory.CreateTextElement(TextElementType.Paragraph);
            PonyTextContext ctx = new DerivedTextContext(textContext, textElement);
            foreach (var item in structureBases) {
                item.Evaluate(ctx);
            }
            textContext.WriteTextElement(textElement);
        }
    }
}
