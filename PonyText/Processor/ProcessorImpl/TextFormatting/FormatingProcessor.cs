// File: FormatingProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Text;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Utils;

namespace PonyText.Processor.ProcessorImpl.TextFormatting {
    [Processor("format")]
    public class FormatingProcessor : AbstractProcessor
    {
        private sbyte[,] transitionTable = new sbyte[,]
        {
            //any \  #
            {  0, 1, 2},
            { -1, 0, 0},
            {  0, 1, 2}
        };

        public FormatingProcessor(ProcessorInfo processorInfo) : base(processorInfo)
        {
            ProcessorParam.Require(StructureType.LiteralStruct).MakeVariable();
        }

        protected override void OnPreProcessInternal(PonyTextStructureBase[] args)
        {
            string format = (string)args[0].GetUnderlyingObject();
            StringBuilder sb = new StringBuilder();
            sbyte currentState = 0;
            for (int i = 0, j = 1; i < format.Length; i++)
            {
                char c = format[i];
                switch (c)
                {
                    case '\\':
                        currentState = transitionTable[currentState, 1];
                        break;
                    case '#':
                        currentState = transitionTable[currentState, 2];
                        break;
                    default:
                        currentState = transitionTable[currentState, 0];
                        break;
                }
                if (currentState == 0)
                {
                    sb.Append(c);
                }
                else if (currentState == 2)
                {
                    if (j >= args.Length)
                    {
                        throw new PreProcessorException("Not enough arguments for doing formating");
                    }
                    if (sb.Length > 0)
                    {
                        WriteText(TextElementFactory.CreateTextElement(TextElementType.TextUnit, sb.ToString()));
                        sb.Clear();
                    }
                    WriteText(GetText(args[j]));
                    j++;
                }
                else if (currentState == -1)
                {
                    throw new PreProcessorException(
                        $"Invalid format for format template string: \"{format}\". " +
                        $"At character '{c}' column {i + 1}");
                }
            }
        }
    }
}
