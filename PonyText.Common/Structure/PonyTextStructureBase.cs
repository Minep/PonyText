// File: PonyTextStructureBase.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Context;
using PonyText.Common.Exceptions;
using PonyText.Common.Parser;

namespace PonyText.Common.Structure
{
    public abstract class PonyTextStructureBase
    {
        public PonyToken AssociatedToken { get; }
        public StructureType StructureType
        {
            get;
        }

        protected PonyTextStructureBase(StructureType structureType, PonyToken token)
        {
            StructureType = structureType;
            this.AssociatedToken = token;
        }

        public void Evaluate(PonyTextContext textContext) {
            try {
                EvaluateInternal(textContext);
            }
            catch(PreProcessorException e) {
                e.AddToStackTrace(PreProcessTrace.CreateTraceTokenLevel(AssociatedToken));
                throw e;
            }
        }

        public abstract void EvaluateInternal(PonyTextContext textContext);
        public virtual object GetUnderlyingObject()
        {
            return null;
        }
    }
}
