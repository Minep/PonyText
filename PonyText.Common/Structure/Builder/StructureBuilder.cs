// File: StructureBuilder.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using PonyText.Common.Exceptions;
using PonyText.Common.Parser;
using PonyText.Common.Structure;

namespace PonyText.Common.Structure.Builder {
    public class StructureBuilder {
        Stack<PonyTextStructureBase> builderStack = new Stack<PonyTextStructureBase>();
        PonyTextStructureBase currentContext;

        public StructureType CurrentContextType => currentContext.StructureType;
        public StructureBuilder() {

        }

        public void AddLiteral(PonyToken token) {
            addToContext(new PonyTextLiteralStruct(token));
        }

        public void AddNumber(PonyToken token) {
            addToContext(new PonyTextNumStruct(token));
        }

        public void AddMarco(PonyToken token) {
            addToContext(new PonyTextMarcoStruct(token));
        }

        public void StartTextStructure(PonyToken token) {
            setContext(new PonyTextStruct(token));
        }

        public void StartDirectiveBlock(PonyToken token) {
            setContext(new PonyTextDirectivesBlock(token));
        }

        public void StartDirective(PonyToken token) {
            setContext(new PonyTextDirective(token));
        }

        public void TryStartParagraph(PonyToken token) {
            if (currentContext.StructureType == StructureType.ParagraphStruct) {
                if (token != null && !string.IsNullOrEmpty(token.Text)) {
                    AddLiteral(token);
                }
            }
            else {
                setContext(new PonyTextParagraphStruct(token));
            }
        }


        public void StartMappingStruct(PonyToken token) {
            setContext(new PonyTextMapStruct(token));
        }

        public void AddLiteralMapping(string key, PonyToken token) {
            addToMappingContext(key, new PonyTextLiteralStruct(token));
        }

        public void AddNumMapping(string key, PonyToken token) {
            addToMappingContext(key, new PonyTextNumStruct(token));
        }

        public void AddMarcoMapping(string key, PonyToken token) {
            addToMappingContext(key, new PonyTextMarcoStruct(token));
        }

        public void StartFormatableMarcoStruct(PonyToken token) {
            setContext(new PonyTextMarcoStruct(token));
        }

        public void EndCurrentContext() {
            var ctx = popContext();
            addToContext(ctx);
        }

        /// <summary>
        /// Build the structure from builder stack
        /// </summary>
        /// <exception cref="PonyTextException"></exception>
        /// <returns></returns>
        public PonyTextStructureBase BuildStructure() {
            if (builderStack.Count == 1) {
                return builderStack.Pop();
            }
            else {
                while (builderStack.Count > 1) {
                    EndCurrentContext();
                }
                return builderStack.Pop();
            }
            throw new PonyTextException(ProcessingStage.Parsing, "Invalid builder stack state.");
        }

        private void setContext(PonyTextStructureBase ctx) {
            currentContext = ctx;
            builderStack.Push(ctx);
        }

        private PonyTextStructureBase popContext() {
            PonyTextStructureBase ctx;
            if (builderStack.TryPop(out ctx) &&
               builderStack.TryPeek(out currentContext)) {
                return ctx;
            }
            throw new PonyTextException(ProcessingStage.Parsing, "Empty or unexpected structure while walking through AST.");
        }

        private void addToContext(PonyTextStructureBase structureBase) {
            if (currentContext == null) {
                throw new InvalidOperationException();
            }
            if (currentContext.StructureType == StructureType.ParagraphStruct) {
                ((PonyTextParagraphStruct)currentContext).AddToParagraph(structureBase);
            }
            else if (currentContext.StructureType == StructureType.DirectiveStruct) {
                ((PonyTextDirective)currentContext).AddArgument(structureBase);
            }
            else if (currentContext.StructureType == StructureType.PTextStruct) {
                ((PonyTextStruct)currentContext).AddStructure(structureBase);
            }
            else if (currentContext.StructureType == StructureType.MarcoStruct) {
                ((PonyTextMarcoStruct)currentContext).AddFormatArgs(structureBase);
            }
            else if (currentContext.StructureType == StructureType.DirectiveBlockStruct) {
                ((PonyTextDirectivesBlock)currentContext).AddDirectives(structureBase as PonyTextDirective);
            }
            else {
                throw new InvalidOperationException();
            }
        }

        private void addToMappingContext(string key, PonyTextStructureBase structureBase) {
            if (currentContext == null) {
                throw new InvalidOperationException();
            }
            if (currentContext.StructureType == StructureType.MapStruct) {
                ((PonyTextMapStruct)currentContext).AddToMap(key, structureBase);
            }
            else {
                throw new InvalidOperationException();
            }
        }
    }
}
