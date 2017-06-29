using System;
using NiceIO;

namespace SourcesCloner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Options.InitAndSetup(args))
                return;

            new Cloner(Options.MonoCheckout.ToNPath()).Run(Options.ReferenceProfile, Options.OutputProfile, Options.CloneInsteadOfInclude, Options.ForceAll);
        }
    }
}
