// File: PdfRender.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.IO;
using System.Reflection;
using PonyText;
using PonyText.Common;
using PonyText.Common.Exceptions;
using PonyText.Common.Misc;
using PonyText.Common.Processor;
using PonyText.Common.Structure;
using PonyText.Processor.FactoryImpl;
using PonyText.Runtime.Context;
using PonyText.Utils;
using PonyTextEntry.CmdOptions;
using PonyTextRenderer.Pdf;

namespace PonyTextEntry.Flows.RenderProcedure {
    public class PdfRender : IRendProcedure {

        TextLogger logger = GlobalConfiguration.Instance.Logger;
        public string GetProcedureDescription() {
            return "Render the ponytext into PDF";
        }

        public void RunProcedure(VerbRenderOpts opts) {

            string docPath = Path.GetFullPath(opts.entryFile);
            logger.LogInfo($"Start rendering...");
            AssemblyInjectionManager injectionManager = new AssemblyInjectionManager();
            injectionManager.RegisterAssembly("PonyTextRenderer.Pdf", Assembly.GetAssembly(typeof(PdfRenderer)));
            injectionManager.RegisterAssembly("PonyText.Processor", Assembly.GetAssembly(typeof(PonyTextFactory)));

            IProcessorFactory processor = new ProcessorFactory(injectionManager);
            SimpleTextContext simpleTextContext = new SimpleTextContext(processor);

            processor.LoadProcessorFrom("PonyText.Processor");

            PonyTextStructureBase structureBase = PonyTextFactory.CreateEvaluable(docPath, simpleTextContext);

            try{
                structureBase.Evaluate(simpleTextContext);

                PdfRenderer pdfRenderer = new PdfRenderer(simpleTextContext);
                simpleTextContext.GetCurrentContext().Render(pdfRenderer, simpleTextContext);

                using (FileStream fs = new FileStream(opts.outputFile, FileMode.Create)) {
                    pdfRenderer.RenderContentTo(fs);
                }
                logger.LogInfo($"Exported to {opts.outputFile}");
            }
            catch(Exception e){
                logger.LogError(e.Message);
                if (e is PreProcessorException) {
                    PreProcessorException ppe = e as PreProcessorException;
                    foreach (var item in ppe.GetPonyTextStackTrace())
                    {
                        logger.LogError(item);
                    }
                }
                logger.LogError($"Error in file {docPath}");
                logger.LogDebug(e.StackTrace);
            }
        }
    }
}
