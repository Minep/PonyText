// File: AssemblyInjectionManager.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PonyText.Utils {
    public class AssemblyInjectionManager {
        Dictionary<string, Assembly> registeredAsm;

        public AssemblyInjectionManager() {
            registeredAsm = new Dictionary<string, Assembly>();
        }

        public void RegisterAssembly(string @namespace, Assembly asm) {
            registeredAsm.TryAdd(@namespace, asm);
        }

        public bool TryGetAssembly(string key, out Assembly asm) {
            return registeredAsm.TryGetValue(key, out asm);
        }
    }
}
