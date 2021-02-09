using System.IO;
using System.Reflection;
using PonyText.Runtime.Common;

namespace PonyText.Common.Context {
    public class ContextMetadata
    {
        public string WorkingDirectory {get; set;}
        public DependencyList DependencyList { get; }
        public ContextMetadata()
        {
            DependencyList = new DependencyList();
            WorkingDirectory = Assembly.GetExecutingAssembly().Location;
        }

        public string GetAbsolutePath(string relativePath) {
            if(Path.IsPathRooted(relativePath)){
                return relativePath;
            }
            return Path.Combine(WorkingDirectory, relativePath);
        }
    }
}