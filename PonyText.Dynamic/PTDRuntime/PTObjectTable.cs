// File: PTObjectTable.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Dynamic.PTDRuntime {
    public class PTObjectTable {
        private static volatile PTObjectTable PTObjectTableInstance = null;
        private static readonly object locker = new object();

        public static PTObjectTable Instance {
            get {
                if (PTObjectTableInstance == null) {
                    lock (locker) {
                        PTObjectTableInstance = PTObjectTableInstance ?? new PTObjectTable();
                    }
                }
                return PTObjectTableInstance;
            }
        }

        Dictionary<string, object> objectTable;

        private PTObjectTable() {
            objectTable = new Dictionary<string, object>();
        }

        public bool ContainObject(string name) {
            return objectTable.ContainsKey(name);
        }

        public bool TryGetObject(string objectName, out object instance) {
            return objectTable.TryGetValue(objectName, out instance);
        }

        public bool TrySetObject(string objectName, object instance) {
            return objectTable.TryAdd(objectName, instance);
        }
    }
}
