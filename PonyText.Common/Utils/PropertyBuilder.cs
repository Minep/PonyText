// File: PropertyBuilder.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Common.Utils {
    public class PropertyBuilder {
        private Dictionary<string, object> propDic;

        public static PropertyBuilder New() {
            return new PropertyBuilder();
        }

        public PropertyBuilder SetProperty(string key, object val) {
            if(!propDic.TryAdd(key, val)) {
                propDic[key] = val;
            }
            return this;
        }

        public Dictionary<string, object> Create() {
            return propDic;
        }
    }
}
