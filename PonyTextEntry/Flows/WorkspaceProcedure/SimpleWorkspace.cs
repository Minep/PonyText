// File: SimpleWorkspace.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PonyTextEntry.CmdOptions;

namespace PonyTextEntry.Flows.WorkspaceProcedure {
    public class SimpleWorkspace : IWsProcedure {

        const string main_template =
            "$\r\n" +
            "    @workingDirectory \"{0}\"\r\n" +
            "    @include \"setting.pony\"\r\n" +
            "$\r\n";

        const string setting_template = "" +
            "$\r\n" +
            "@set \"document.layout\" {{\r\n" +
            "    \"pageSize\": \"A4\"\r\n" +
            "    \"margin\": \"1.27cm\"\r\n" +
            "    \"orientation\": \"portrait\"\r\n" +
            "}}\r\n" +
            "@set \"document.info\" {{\r\n" +
            "    \"title\": \"{0}\"\r\n" +
            "    \"author\": \"{1}\"\r\n" +
            "}}\r\n" +
            "@set \"document.style\" {{\r\n" +
            "    \"fontSize\": \"10.5\"\r\n" +
            "    \"fontFamily\": \"Arial\"\r\n" +
            "    \"fontColor\": \"#000\"\r\n" +
            "    \"firstLineIndent\": \"2em\"\r\n" +
            "}}\r\n" +
            "$\r\n";

        public string GetProcedureDescription() {
            return "A simple setup, contains, a entry file and config file";
        }

        public void RunProcedure(VerbCreateWorkspaceOpts ws) {
            string absPath = Path.GetFullPath(ws.TargetDirectory);
            if (!Directory.Exists(absPath)) {
                Directory.CreateDirectory(absPath);
            }
            using(FileStream fs = new FileStream(Path.Combine(absPath, "main.pony"), FileMode.Create)) {
                using(StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(main_template, absPath.Replace("\\", "\\\\"));
                }
            }
            using (FileStream fs = new FileStream(Path.Combine(absPath, "setting.pony"), FileMode.Create)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(setting_template, ws.Title, ws.Author);
                }
            }
        }
    }
}
