// File: IWsProcedure.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyTextEntry.CmdOptions;

namespace PonyTextEntry.Flows.WorkspaceProcedure {
    public interface IWsProcedure {
        public void RunProcedure(VerbCreateWorkspaceOpts ws);
        public string GetProcedureDescription();
    }
}
