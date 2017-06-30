using System;
using System.Collections.Generic;
using System.IO;
using NiceIO;
using Utilities;

namespace SourcesCloner
{
    public class Cloner
    {
        private readonly MonoCheckout _monoCheckout;

        public Cloner(NPath monoCheckoutRoot)
        {
            _monoCheckout = new MonoCheckout(monoCheckoutRoot);
        }

        public void Run(string referenceProfile, string outputProfile, string altIncludeProfile, bool cloneInsteadOfInclude, bool forceAll)
        {
            foreach (var assemblyDirectory in _monoCheckout.Root.Combine("mcs", "class").Directories())
            {
                var referenceProfileSourcesFile = _monoCheckout.ReferenceSourcesFilePathFor(assemblyDirectory, referenceProfile);

                // The reference profile must not include this assembly, nothing to do
                if (!referenceProfileSourcesFile.FileExists())
                    continue;

                var outputProfileSourcesFile = _monoCheckout.OutputProfileSourcesFilePathFor(assemblyDirectory, outputProfile);

                if (outputProfileSourcesFile.FileExists())
                {
                    Console.WriteLine($"Deleting existing output profile sources file : {outputProfileSourcesFile}");
                    outputProfileSourcesFile.Delete();
                }

                if (cloneInsteadOfInclude)
                {
                    referenceProfileSourcesFile.Copy(outputProfileSourcesFile);
                    Console.WriteLine($"Created sources file : {outputProfileSourcesFile}");
                }
                else
                {
                    if (!forceAll && !referenceProfileSourcesFile.FileName.StartsWith(referenceProfile))
                    {
                        Console.WriteLine($"Skipping sources file.  Profile specific sources not needed : {outputProfileSourcesFile}");
                        continue;
                    }

                    var includeFileName = referenceProfileSourcesFile.FileName;

                    if (!string.IsNullOrEmpty(altIncludeProfile))
                    {
                        var altPath = _monoCheckout.OutputProfileSourcesFilePathFor(assemblyDirectory, altIncludeProfile);
                        if (altPath.FileExists())
                            includeFileName = altPath.FileName;
                    }

                    using (var writer = new StreamWriter(outputProfileSourcesFile.ToString()))
                    {
                        writer.Write($"#include {includeFileName}\n");
                    }
                    Console.WriteLine($"Created sources file : {outputProfileSourcesFile}");
                }
            }
        }
    }
}
