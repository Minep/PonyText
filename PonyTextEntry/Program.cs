using CommandLine;
using PonyTextEntry.CmdOptions;
using PonyTextEntry.Flows;

namespace PonyTextEntry {
    class Program
    {
        static void Main(string[] args) {
            Parser.Default.ParseArguments<VerbRenderOpts, VerbCreateWorkspaceOpts>(args)
                .WithParsed<VerbCreateWorkspaceOpts>(opt => {
                    Workspace.Create(opt);
                })
                .WithParsed<VerbRenderOpts>(opt => {
                    Render.Process(opt);
                });
        }
    }
}
