// File: PonyTextDirectivesBlock.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Parser;
using PonyText.Common.Text;

namespace PonyText.Common.Structure {
    public class PonyTextDirectivesBlock : PonyTextStructureBase {
        //Since it is a directive block, then only directive allowed.
        List<PonyTextDirective> textDirectives;
        public PonyTextDirectivesBlock(PonyToken token) : base(StructureType.DirectiveBlockStruct, token) {
            textDirectives = new List<PonyTextDirective>();
        }


        public void AddDirectives(PonyTextDirective textDirective) {
            textDirectives.Add(textDirective);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            foreach (var directive in textDirectives) {
                textContext.WriteTextElement(TextElementFactory.CreateTextElement(TextElementType.Paragraph));
                directive.Evaluate(textContext);
            }
        }
    }
}
