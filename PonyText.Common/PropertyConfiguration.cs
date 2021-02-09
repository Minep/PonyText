// File: PropertyConfiguration.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using System.Text;
using PonyText.Common.Renderer;

namespace PonyText.Common
{
    public class PropertyConfiguration : IPropertyCollection
    {
        private Dictionary<string, object> propertyConfig;

        public PropertyConfiguration()
        {
            propertyConfig = new Dictionary<string, object>();
        }

        public void SetProperty(string key, object value)
        {
            if (propertyConfig.ContainsKey(key))
            {
                propertyConfig[key] = value;
            }
            else
            {
                propertyConfig.Add(key, value);
            }
        }

        public void AppendProperty(string key, object value) {
            if (propertyConfig.ContainsKey(key)) {
                object val = propertyConfig[key];
                if(val is List<object>) {
                    ((List<object>)val).Add(value);
                }
                else {
                    propertyConfig[key] = new List<object>() { value };
                }
            }
            else {
                List<object> properties = new List<object>();
                properties.Add(value);
                propertyConfig.Add(key, properties);
            }
        }

        public void Merge(Dictionary<string, object> properties)
        {
            foreach (var item in properties)
            {
                if (propertyConfig.ContainsKey(item.Key))
                {
                    object obj = propertyConfig[item.Key];
                    if(obj is List<object>) {
                        List<object> lst =  (List<object>)obj;
                        if(item.Value is List<object>) {
                            lst.AddRange((List<object>)item.Value);
                        }
                        else {
                            lst.Add(item.Value);
                        }
                    }
                    else {
                        propertyConfig[item.Key] = item.Value;
                    }
                }
                else
                {
                    propertyConfig.Add(item.Key, item.Value);
                }
            }
        }

        public object GetProperty(string key)
        {
            if (propertyConfig.ContainsKey(key))
            {
                return propertyConfig[key];
            }
            else
            {
                return null;
            }
        }

        public bool TryGetProperty<T>(string key, out T val)
        {
            if (propertyConfig.ContainsKey(key))
            {
                object o = propertyConfig[key];
                if (o is T)
                {
                    val = (T)o;
                    return true;
                }
            }
            val = default(T);
            return false;
        }

        public int CountOfProperties => propertyConfig.Count;

        public bool HasProperty(string key)
        {
            return propertyConfig.ContainsKey(key);
        }

        public object this[string index]
        {
            get => propertyConfig[index];
        }

        public override string ToString() {
            List<string> strs = new List<string>();
            foreach (var item in propertyConfig) {
                string s = ($"\"{item.Key}\":");
                if(item.Value is string) {
                    s += $"\"{item.Value}\"";
                }
                else {
                    s += item.Value.ToString();
                }
                strs.Add(s);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendJoin(',', strs);
            sb.Append("}");
            return sb.ToString();
        }

        public Dictionary<string, object> ToDictionary() {
            return propertyConfig;
        }
    }
}
