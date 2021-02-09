// File: VerbalizationProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Utils;
using PonyText.Utils.Verbolization;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("readNum")]
    public class VerbalizationProcessor : AbstractProcessor {

        Dictionary<VerbalizationOptions, INumberVerbalizer> verbalizers = new Dictionary<VerbalizationOptions, INumberVerbalizer>() {
            {VerbalizationOptions.Chinese, new ChineseVerbalizer() }
        };

        public VerbalizationProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam
                .Require(StructureType.NumberStruct)
                .Require(StructureType.LiteralStruct)
                .RequireOptional(StructureType.LiteralStruct);
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            decimal number = (decimal)args[0].GetUnderlyingObject();
            string lang = args[1].GetUnderlyingObject() as string;
            VerbalizationOptions language = (VerbalizationOptions)Enum.Parse(typeof(VerbalizationOptions), lang);
            VerbalizationMode mode = VerbalizationMode.ReadOut;
            if(args.Length == 3) {
                mode = (VerbalizationMode)Enum.Parse(typeof(VerbalizationMode), args[2].GetUnderlyingObject() as string);
            }
            WriteTextLiteral(VerbalizerManager.Instance.Verbalize(number.ToString(), language, mode));
        }
    }
}
