// File: MacroTable.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Exceptions;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Common.Text;
using PonyText.Common.Utils;

namespace PonyText.Common.Context
{
    public class MacroTable
    {
        Dictionary<string, PonyTextStructureBase> marcos;

        public MacroTable()
        {
            marcos = new Dictionary<string, PonyTextStructureBase>();
        }

        public void PutMarco(string name, PonyTextStructureBase value)
        {
            if (!marcos.ContainsKey(name))
            {
                marcos.Add(name, value);
            }
            else
            {
                marcos[name] = value;
            }
        }

        public int Count => marcos.Count;

        public bool ContainsMarco(string name)
        {
            return marcos.ContainsKey(name);
        }

        public PonyTextStructureBase GetMarco(string name)
        {
            if (marcos.ContainsKey(name))
            {
                return marcos[name];
            }
            throw new PreProcessorException($"Marco \"{name}\" not found");
        }
    }
}
