// File: VerbCompile.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace PonyTextEntry.CmdOptions {
    [Verb("publish", HelpText = "Publish the PonyText project into supported document format")]
    public class VerbRenderOpts {
        [Option('e', "entry" ,HelpText = "Specified a new entry file", Default = "main.pony")]
        public string entryFile { get; set; }

        [Option('f', "format", HelpText = "Output document format", Default = "pdf")]
        public string format { get; set; }

        [Option("list-formats", HelpText = "List all supported formats", Default = false)]
        public bool shouldListFormats { get; set; }

        [Option('o', "output", HelpText = "Output document path", Required = true)]
        public string outputFile { get; set; }
    }
}
