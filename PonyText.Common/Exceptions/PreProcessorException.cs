// File: PreProcessorException.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using System.Text;
using PonyText.Common.Parser;

namespace PonyText.Common.Exceptions
{
    public class PreProcessorException : PonyTextException
    {
        public string ProcessorName { get; private set; }
        protected List<PreProcessTrace> stackTrace;
        public PreProcessorException(string cause) : base(ProcessingStage.PreProcessing, cause)
        {
            stackTrace = new List<PreProcessTrace>();
        }
        private bool isProcessorSet = false;
        public void SetProcessor(string name) {
            if (!isProcessorSet) {
                ProcessorName = name;
                isProcessorSet = true;
            }
        }

        public void AddToStackTrace(PreProcessTrace processTrace) {
            stackTrace.Add(processTrace);
        }

        public PreProcessorException(string processorName, string cause) : base(ProcessingStage.PreProcessing, cause)
        {
            stackTrace = new List<PreProcessTrace>();
            ProcessorName = processorName;
            AddToStackTrace(new PreProcessTrace(PonyToken.Empty, processorName, null));
        }

        public List<string> GetPonyTextStackTrace() {
            List<string> sb = new List<string>();
            foreach (var item in stackTrace) {
                string itemStr = item.ToString();
                if(!string.IsNullOrEmpty(itemStr)){
                    sb.Add(">>> "+itemStr);
                }
            }
            return sb;
        }
        public override string Message => $"[{stage}](Processor: {ProcessorName}) {cause}";
    }
}
