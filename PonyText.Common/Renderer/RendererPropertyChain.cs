// File: RendererPropertyChain.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Common.Renderer
{
    public class RendererPropertyChain : IPropertyCollection
    {
        private LinkedList<PropertyConfiguration> propertyChain;
        public RendererPropertyChain()
        {
            propertyChain = new LinkedList<PropertyConfiguration>();
        }

        public void PushPropertyContext(PropertyConfiguration configuration)
        {
            propertyChain.AddLast(configuration);
        }

        public void PopPropertyContext()
        {
            if (propertyChain.Count > 0)
            {
                propertyChain.RemoveLast();
            }
        }

        public PropertyConfiguration CurrentContextConfiguration => propertyChain.Last.Value;

        public int Length => propertyChain.Count;

        public bool TryGetProperty<T>(string key, out T value)
        {
            LinkedListNode<PropertyConfiguration> node = propertyChain.Last;
            while (node != null)
            {
                if (node.Value.HasProperty(key))
                {
                    return node.Value.TryGetProperty(key, out value);
                }
                node = node.Previous;
            }
            value = default(T);
            return false;
        }
    }
}
