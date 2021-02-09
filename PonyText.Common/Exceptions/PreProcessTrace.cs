// File: PreProcessTrace.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyText.Common.Parser;
using PonyText.Common.Processor;

namespace PonyText.Common.Exceptions {
    public class PreProcessTrace {
        PonyToken erroneousToken;
        string erroneousProcessor;
        string erroneousFilename;

        public static PreProcessTrace CreateTrace(string erroneousProcessor) {
            return new PreProcessTrace(PonyToken.Empty, erroneousProcessor, string.Empty);
        }

        public static PreProcessTrace CreateTraceFileNameLevel(string fileName) {
            return new PreProcessTrace(PonyToken.Empty, fileName);
        }

        public static PreProcessTrace CreateTraceTokenLevel(PonyToken token) {
            return new PreProcessTrace(token);
        }

        public PreProcessTrace(PonyToken erroneousToken, string erroneousProcessor, string erroneousFilename) {
            this.erroneousToken = erroneousToken;
            this.erroneousProcessor = erroneousProcessor;
            this.erroneousFilename = erroneousFilename;
        }

        public PreProcessTrace(PonyToken erroneousToken, string erroneousFilename) {
            this.erroneousToken = erroneousToken;
            this.erroneousFilename = erroneousFilename;
        }

        public PreProcessTrace(PonyToken erroneousToken) {
            this.erroneousToken = erroneousToken;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            if(erroneousToken!=null && erroneousToken.TokenType != PonyTokenType.EMPTY) {
                sb.Append($"Near Row={erroneousToken.Row}, Column={erroneousToken.Column}, ");
                sb.Append($"Token={erroneousToken.TokenType} ");
                if(!string.IsNullOrEmpty(erroneousToken.Text)){
                    sb.Append($"(Lexeme: \"{truncate(erroneousToken.Text)}\")");
                }
            }
            if (!string.IsNullOrEmpty(erroneousFilename)) {
                sb.Append($"in file \"{erroneousFilename}\" ");
            }
            if (!string.IsNullOrEmpty(erroneousProcessor)) {
                sb.Append($"initiated by processor: \"{erroneousProcessor}\"");
            }
            return sb.ToString();
        }

        private string truncate(String str){
            if(str.Length>15){
                return str.Substring(0,15) + "...";
            }
            return str;
        }
    }
}
