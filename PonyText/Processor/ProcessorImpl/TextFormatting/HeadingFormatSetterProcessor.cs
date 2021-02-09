// File: HeadingFormatSetterProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Processor.Misc;
using PonyText.Utils;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {

    [Processor("headingConfig")]
    public class HeadingFormatSetterProcessor : AbstractProcessor {
        HeaderCounter headerCounter;
        public HeadingFormatSetterProcessor(ProcessorInfo processorInfo) : base(processorInfo) {
            ProcessorParam.Require(StructureType.MapStruct);
            headerCounter = HeaderCounter.Instance;
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args) {
            Dictionary<string, object> config = CreatePrimitiveMapping(args[0]);
            
            config.ApplyConfigTo<string>(HeaderCounter.ConfigEnableNumbering, (val) => {
                headerCounter.EnableNumbering = val.Equals("true");
            });

            config.ApplyConfigTo<decimal>(HeaderCounter.ConfigStartingNumber, (val) => {
                headerCounter.StartNumber = decimal.ToInt32(val);
                headerCounter.ResetAll();
            });

            foreach (var item in config) {
                if (item.Key.StartsWith(HeaderCounter.ConfigLevel)) {
                    string level = item.Key[HeaderCounter.ConfigLevel.Length..];
                    int level_parsed = int.Parse(level) - 1;
                    headerCounter.SetHeadingFormat(level_parsed, item.Value as string);
                }
            }
        }
    }
}
