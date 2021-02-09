// File: AbstractProcessor.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using PonyText.Common.Context;
using PonyText.Common.Exceptions;
using PonyText.Common.Misc;
using PonyText.Common.Renderer;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Text.Impl;
using PonyText.Common.Utils;

namespace PonyText.Common.Processor {
    public abstract class AbstractProcessor {
        public const string RENDER_PROCESSOR_PROPERTY = "__PROCESSOR";
        protected PonyTextContext ctx;

        public ProcessorParamConfig ProcessorParam { get; }
        public ProcessorAttribute ProcessorAttribute {
            get;
        }

        protected TextLogger logger;

        public AbstractProcessor(ProcessorInfo processorInfo) {
            ProcessorParam = new ProcessorParamConfig();
            logger = GlobalConfiguration.Instance.Logger;
            if (processorInfo != null) {
                ProcessorAttribute = processorInfo.attribute;
            }
        }

        public void Preporcess(PonyTextContext context, PonyTextStructureBase[] args) {
            ctx = context;
            try {
                ProcessorParam?.ValidateParameter(args);
                OnPreProcessInternal(args);
            }
            catch (Exception e) {
                if(e is PreProcessorException) {
                    PreProcessorException ppe = (PreProcessorException) e;
                    ppe.AddToStackTrace(PreProcessTrace.CreateTrace(ProcessorAttribute.ProcessorName));
                    throw ppe;
                }
                else {
                    throw new PreProcessorException(ProcessorAttribute.ProcessorName, e.Message);
                }
            }
        }

        public void PreRendering(PonyTextContext context, AbstractRendererBase rendererBase, AbstractTextElement textElement) {
            ctx = context;
            OnPreRenderInternal(textElement, rendererBase);
        }

        public void PostRendering(PonyTextContext context, AbstractRendererBase rendererBase, AbstractTextElement textElement) {
            ctx = context;
            OnPostRenderInternal(textElement, rendererBase);
        }

        protected virtual void OnPreRenderInternal(AbstractTextElement textElement, AbstractRendererBase rendererBase) {

        }

        protected virtual void OnPostRenderInternal(AbstractTextElement textElement, AbstractRendererBase rendererBase) {

        }
        protected abstract void OnPreProcessInternal(PonyTextStructureBase[] args);

        protected bool CheckStructType(PonyTextStructureBase structureBase, StructureType desiredType) {
            return structureBase.StructureType == desiredType;
        }

        protected void WriteTextLiteral(string literal, bool avaliableInRenderSatge = false) {

            AbstractTextElement textElement = TextElementFactory.CreateTextElement(TextElementType.TextUnit, literal);
            WriteText(textElement, avaliableInRenderSatge);
        }

        protected void WriteText(AbstractTextElement text, bool avaliableInRenderSatge = false) {
            if (avaliableInRenderSatge) {
                text.CustomProperty.SetProperty(RENDER_PROCESSOR_PROPERTY, ProcessorAttribute.ProcessorName);
            }
            ctx.WriteTextElement(text);
        }

        protected AbstractTextElement GetText(PonyTextStructureBase structureBase) {
            if (structureBase.StructureType == StructureType.NumberStruct ||
               structureBase.StructureType == StructureType.LiteralStruct) {
                string obj = structureBase.GetUnderlyingObject().ToString();
                return TextElementFactory.CreateTextElement(TextElementType.TextUnit, obj);
            }
            // else if (structureBase.StructureType == StructureType.MarcoStruct) {
            //     PonyTextMarcoStruct marcoStruct = structureBase as PonyTextMarcoStruct;
            //     return GetText(ctx.MacroTable.GetMarco(marcoStruct.MarcoName));
            // }
            else if (structureBase.StructureType == StructureType.MapStruct) {
                throw new PreProcessorException($"Not support type.");
            }
            else if (structureBase.StructureType == StructureType.DirectiveStruct) {
                PonyTextContext newCtx = new DerivedTextContext(ctx,
                        TextElementFactory.CreateTextElement(TextElementType.TextElementCollection));
                structureBase.Evaluate(newCtx);
                TextElementCollection elementCollection = (TextElementCollection)newCtx.GetCurrentContext();
                if (elementCollection.getDocument().Count == 1) {
                    return elementCollection.getDocument()[0];
                }
                return elementCollection;
            }
            else {
                PonyTextContext newCtx = new DerivedTextContext(ctx,
                        TextElementFactory.CreateTextElement(TextElementType.TextElementCollection));
                structureBase.Evaluate(newCtx);
                return newCtx.GetCurrentContext();
            }
        }

        protected Dictionary<string, object> CreatePrimitiveMapping(PonyTextStructureBase mapStruct) {
            if (mapStruct.StructureType != StructureType.MapStruct) {
                return null;
            }
            PonyTextMapStruct pony = (PonyTextMapStruct)mapStruct;
            Dictionary<string, object> mappings = new Dictionary<string, object>();
            foreach (var kvpair in pony.GetMappings()) {
                switch (kvpair.Value.StructureType) {
                    case StructureType.MarcoStruct:
                        mappings.Add(kvpair.Key, GetText(kvpair.Value));
                        break;
                    case StructureType.NumberStruct:
                        mappings.Add(kvpair.Key, kvpair.Value.GetUnderlyingObject());
                        break;
                    case StructureType.LiteralStruct:
                        mappings.Add(kvpair.Key, kvpair.Value.GetUnderlyingObject());
                        break;
                    default:
                        throw new PreProcessorException($"Mapping can only contain primitive type, but {kvpair.Value.StructureType} was found.");
                }
            }
            return mappings;
        }

        public void WriteDirectives(params string[] directives) {
            TextDirectives textDirectives = new TextDirectives();
            textDirectives.AddDirectives(directives);
            ctx.WriteTextElement(textDirectives);
        }
    }
}
