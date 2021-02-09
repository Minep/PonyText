// File: IRendProcedure.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;
using PonyTextEntry.CmdOptions;

namespace PonyTextEntry.Flows.RenderProcedure {
    public interface IRendProcedure {
        public void RunProcedure(VerbRenderOpts opts);
        public string GetProcedureDescription();
    }
}
