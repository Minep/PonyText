// File: VerbCreateWorkspace.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace PonyTextEntry.CmdOptions {
    [Verb("create", HelpText = "generate a workspace setup")]
    public class VerbCreateWorkspaceOpts {

        [Option("at", Default = "", Required = false, HelpText = "path to the folder of workspace")]
        public string TargetDirectory { get; set; }

        [Option("workspaceType", Default = "simple", Required = false, HelpText = "Specify a built-in setup plan for workspace")]
        public string WorkspaceType { get; set; }

        [Option("show-workspace-type",Default = false, Required = false, HelpText = "Show all built-in setup plans for workspace")]
        public bool IsShowAvailableWorkspaceCfg { get; set; }

        [Option("title", Default = "Un-named", Required = false, HelpText = "Title of the document")]
        public string Title { get; set; }

        [Option("author", Default = "Anonymous", Required = false, HelpText = "Author of the document")]
        public string Author { get; set; }
    }
}
