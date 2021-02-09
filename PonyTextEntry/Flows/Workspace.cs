// File: Workspace.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyTextEntry.CmdOptions;
using PonyTextEntry.Flows.WorkspaceProcedure;

namespace PonyTextEntry.Flows {
    public class Workspace {

        public static void Create(VerbCreateWorkspaceOpts createWorkspace) {
            new Workspace(createWorkspace).DoAction();
        }

        private VerbCreateWorkspaceOpts workspace;
        private Dictionary<string, IWsProcedure> workspaceTypes = new Dictionary<string, IWsProcedure> {
            {"simple", new SimpleWorkspace() }
        };

        public Workspace(VerbCreateWorkspaceOpts workspace) {
            this.workspace = workspace;
        }

        public void DoAction() {
            if (workspace.IsShowAvailableWorkspaceCfg) {
                foreach (var item in workspaceTypes) {
                    Console.WriteLine("{0}\t\t{1}", item.Key, item.Value.GetProcedureDescription());
                }
                return;
            }
            if (workspaceTypes.ContainsKey(workspace.WorkspaceType)) {
                workspaceTypes[workspace.WorkspaceType].RunProcedure(workspace);
            }
            else {
                Console.WriteLine("Unknow workspace type: {0}", workspace.WorkspaceType);
            }
        }
    }
}
