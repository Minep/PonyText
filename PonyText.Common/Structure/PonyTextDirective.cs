// File: PonyTextDirective.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Parser;
using PonyText.Common.Processor;

namespace PonyText.Common.Structure {
    public class PonyTextDirective : PonyTextStructureBase {
        public List<PonyTextStructureBase> arguments;
        public string name;
        public PonyTextDirective(PonyToken token) : base(StructureType.DirectiveStruct, token) {
            arguments = new List<PonyTextStructureBase>();
            name = token.Text;
        }

        public void SetDirective(string name) {
            this.name = name;
        }

        public void AddArgument(PonyTextStructureBase arg) {
            arguments.Add(arg);
        }

        public override void EvaluateInternal(PonyTextContext textContext) {
            if (!textContext.MacroTable.ContainsMarco(name)) {
                var processor = textContext.ProcessorFactory.GetPreProcessor(name);
                processor.Preporcess(textContext, ToParameter(textContext));
            }
            else {
                var structureBase = textContext.MacroTable.GetMarco(name);
                PonyTextStructureBase[] param = ToParameter(textContext);
                for (var i = 0; i < arguments.Count; i++) {
                    textContext.MacroTable.PutMarco($"_{i + 1}", param[i]);
                }
                structureBase.Evaluate(textContext);
            }
        }

        public PonyTextStructureBase[] ToParameter(PonyTextContext ctx){
            PonyTextStructureBase[] arr = new PonyTextStructureBase[arguments.Count];
            for (int i = 0; i < arguments.Count; i++)
            {
                PonyTextStructureBase structBase = arguments[i];
                if(structBase.StructureType == StructureType.MarcoStruct){
                    arr[i] = ctx.MacroTable.GetMarco(((PonyTextMarcoStruct) structBase).MarcoName);
                }
                else{
                    arr[i] = structBase;
                }
            }
            return arr;
        }
    }
}
