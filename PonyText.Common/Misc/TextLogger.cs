using System;
using System.IO;

namespace PonyText.Common.Misc {
    public class TextLogger
    {
        private StreamWriter writer;
        public TextLogger(StreamWriter writer)
        {
            this.writer = writer;
        }

        public void LogError(string errmsg) {
            writer.WriteLine("[ERROR]({1:HH:mm:ss.ffff}) {0}", errmsg, DateTime.Now);
        }

        public void LogWarning(string msg) {
            writer.WriteLine("[WARN]({1:HH:mm:ss.ffff}) {0}", msg, DateTime.Now);
        }

        public void LogInfo(string msg) {
            writer.WriteLine("[INFO]({1:HH:mm:ss.ffff}) {0}", msg, DateTime.Now);
        }

        public void LogDebug(string msg) {
            writer.WriteLine("[DEBUG]({1:HH:mm:ss.ffff}) {0}", msg, DateTime.Now);
        }
    }
}
