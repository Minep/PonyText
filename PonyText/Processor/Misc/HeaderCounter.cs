using System;
using System.Text;
using PonyText.Utils;
using PonyText.Utils.Verbolization;

namespace PonyText.Processor.Misc {
    public class HeaderCounter {

        private static volatile HeaderCounter HeaderCounterInstance = null;
        private static readonly object locker = new object();

        public static HeaderCounter Instance {
            get {
                if (HeaderCounterInstance == null) {
                    lock (locker) {
                        HeaderCounterInstance = HeaderCounterInstance ?? new HeaderCounter();
                    }
                }
                return HeaderCounterInstance;
            }
        }
        const int HEADING_LEVELS = 6;
        // Six heading level
        private int[] headingCounter = new int[HEADING_LEVELS];
        /*
         * Heading format
         *   I : Numeric Integer
         *   C : Chinese verbalized numeric value
         *   E : English verbalized numeric value
         *   ? : Do not insert number here, just a placeholder
         */
        private string[] headingFormat = new string[6] {
            "I", "I.I", "I.I.I", "I.I.I.I", "I.I.I.I.I", "I.I.I.I.I.I"
        };

        public const string ConfigEnableNumbering = "enableNumbering";
        public const string ConfigStartingNumber = "startFrom";
        public const string ConfigLevel = "headerFmt#";

        private HeaderCounter() {
            //TODO Constructor code here
        }

        public int StartNumber { get; set; } = 0;
        public bool EnableNumbering { get; set; } = true;

        public void Count(int level) {
            if(0<= level && level < HEADING_LEVELS) {
                headingCounter[level]++;
                for (int i = level + 1; i < HEADING_LEVELS; i++) {
                    headingCounter[i] = StartNumber;
                }
            }
        }
        public void Reset(int level) {
            if (0 <= level && level < HEADING_LEVELS) {
                for (int i = level; i < headingCounter.Length; i++) {
                    headingCounter[i] = StartNumber;
                }
            }
        }

        public void ResetAll() {
            Reset(0);
        }

        public int GetHeadingCount(int level) {
            if (0 <= level && level < HEADING_LEVELS) {
                return headingCounter[level];
            }
            return -1;
        }

        public void SetHeadingFormat(int level, string fmt) {
            if(0<=level && level < HEADING_LEVELS) {
                if (string.IsNullOrEmpty(fmt)) {
                    throw new ArgumentException("Invalid format, must be not null and non-empty string");
                }
                headingFormat[level] = fmt;
            }
            else {
                throw new ArgumentException($"Heading with level {level + 1} is not exist");
            }
        }

        public int[] GetAllPrecedingHeadingCount(int level) {
            if (0 <= level && level < HEADING_LEVELS) {
                int[] headings = new int[level + 1];
                for (int i = 0; i <= level; i++) {
                    headings[i] = headingCounter[i];
                }
                return headings;
            }
            return new int[0];
        }

        public string getHeadingNumbering(int level) {
            if (0 <= level && level < HEADING_LEVELS) {
                return parseHeadingFormat(headingFormat[level]);
            }
            return string.Empty;
        }

        const int START = 0, ESC = 1;
        private string parseHeadingFormat(string fmt) {
            int currentState = START;
            int count = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fmt.Length; i++) {
                char c = fmt[i];
                if(currentState == ESC) {
                    sb.Append(c);
                    currentState = START;
                    continue;
                }
                if (c == '\\') {
                    currentState = ESC;
                    continue;
                }
                if (count >= HEADING_LEVELS && (c == 'I' || c == 'C' || c == 'E' || c == '?')) {
                    throw new FormatException($"Invalid heading format string \"{fmt}\"");
                }
                sb.Append(doReplace(c, ref count));
            }
            return sb.ToString();
        }

        private string doReplace(char mode, ref int count) {
            switch (mode) {
                case 'I':
                    return headingCounter[count++].ToString();
                case 'C':
                    return VerbalizerManager
                                .Instance
                                .Verbalize(
                                    headingCounter[count++].ToString(), 
                                    VerbalizationOptions.Chinese, 
                                    VerbalizationMode.ReadOut);
                case 'E':
                    //TODO implement English verbalization
                    return headingCounter[count++].ToString();
                case '?':
                    count++;
                    return string.Empty;
                default:
                    return mode.ToString();
            }
        }
    }
}
