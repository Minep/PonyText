// File: Render.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PonyTextEntry.CmdOptions;
using PonyTextEntry.Flows.RenderProcedure;
using PonyTextEntry.Flows.WorkspaceProcedure;

namespace PonyTextEntry.Flows {
    public class Render {
        public static void Process(VerbRenderOpts opts) {
            new Render(opts).DoAction();
        }


        private VerbRenderOpts renderOpts;

        public Render(VerbRenderOpts renderOpts) {
            this.renderOpts = renderOpts;
        }

        private Dictionary<string, IRendProcedure> supportedFormats = new Dictionary<string, IRendProcedure> {
            {"pdf", new PdfRender() }
        };
        public void DoAction() {
            if (renderOpts.shouldListFormats) {
                foreach (var item in supportedFormats) {
                    Console.WriteLine("{0}\t\t{1}", item.Key, item.Value.GetProcedureDescription());
                }
                return;
            }
            if (supportedFormats.ContainsKey(renderOpts.format)) {
                TimeAction(() => supportedFormats[renderOpts.format].RunProcedure(renderOpts));
            }
            else {
                Console.WriteLine("Unknow format: {0}", renderOpts.format);
            }
        }

        private void TimeAction(Action task) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            task.Invoke();
            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed.ToString("G"));
        }
    }
}
