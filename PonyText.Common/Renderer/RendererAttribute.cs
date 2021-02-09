// File: RendererAttribute.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Common.Renderer
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RendererAttribute : Attribute
    {
        readonly string rendererName;

        public RendererAttribute(string rendererName)
        {
            this.rendererName = rendererName;

        }

        public string RendererName
        {
            get { return rendererName; }
        }
    }
}
