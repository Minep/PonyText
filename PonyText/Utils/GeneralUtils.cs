// File: GeneralUtils.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Utils {
    public static class GeneralUtils {
        public static void ApplyConfigTo<T>(this Dictionary<string,object> mapper, string configField, Action<T> setter) {
            object v;
            if(mapper.TryGetValue(configField, out v) && v.GetType().IsEquivalentTo(typeof(T))) {
                setter((T)Convert.ChangeType(v, typeof(T)));
            }
        }
    }
}
