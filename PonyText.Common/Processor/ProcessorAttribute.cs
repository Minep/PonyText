// File: ProcessorAttribute.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using PonyText.Common.Text.Impl;

namespace PonyText.Common.Processor {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ProcessorAttribute : Attribute
    {
        public string ProcessorName { get; }
        public string Description { get; set; }

        /// <summary>
        /// Indicate whether this processor is also responsible for
        /// rendering stage. If processor write <see cref="Text.Impl.TextUnit"/> using <see cref="AbstractProcessor.WriteText(TextUnit, bool)"/>
        /// with second parameter set to true. Then processor will miss it if this field not set to true
        /// </summary>
        public bool AlsoForRender { get; set; } = false;

        /// <summary>
        /// If true, then only one instance will be created during a entire session.
        /// Otherwise, new instance will be returned for each calling of this processor.
        /// NB. Default to true, due to performance consideration.
        /// </summary>
        public bool Singleton { get; set; } = true;

        public ProcessorAttribute(string directiveName)
        {
            ProcessorName = directiveName;
        }
    }
}
