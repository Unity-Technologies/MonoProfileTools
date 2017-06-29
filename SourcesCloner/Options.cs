using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Options;

namespace SourcesCloner
{
    [ProgramOptions]
    public static class Options
    {
        [HelpDetails("Path to the mono checkout")]
        public static string MonoCheckout;

        [HelpDetails("The name of the mono implementation profile to base the new profile off of.  Defaults to 'net_4_x'")]
        public static string RefProfile;

        [HelpDetails("The name of the output profile that the sources files are generated for.  Defaults to 'unitystandard'")]
        public static string OutProfile;

        [HelpDetails("Optional.  Copy the contents of the reference profile instead of generating sources files that #include the reference profile")]
        public static bool CloneInsteadOfInclude;

        [HelpDetails("Optional.  Output a sources file for every assembly even if the reference profiles will automatically be used")]
        public static bool ForceAll;

        public static void SetToDefaults()
        {
            MonoCheckout = null;
            RefProfile = "net_4_x";
            OutProfile = null;
            CloneInsteadOfInclude = false;
            ForceAll = false;
        }

        public static string NameFor(string fieldName)
        {
            return OptionsParser.OptionNameFor(typeof(Options), fieldName);
        }

        public static bool InitAndSetup(string[] args)
        {
            SetToDefaults();

            if (OptionsParser.HelpRequested(args))
            {
                OptionsParser.DisplayHelp(typeof(Program).Assembly, false);
                return false;
            }

            var unknownArgs = OptionsParser.Prepare(args, typeof(Program).Assembly, false).ToList();

            if (unknownArgs.Count > 0)
            {
                Console.WriteLine("Unknown arguments : ");
                foreach (var remain in unknownArgs)
                {
                    Console.WriteLine("\t {0}", remain);
                }

                return false;
            }

            if (!ValidateArguments())
                return false;

            return true;
        }

        private static bool ValidateArguments()
        {
            if (string.IsNullOrEmpty(OutProfile))
            {
                Console.WriteLine($"Missing required argument {NameFor(nameof(OutProfile))}");
                return false;
            }

            return true;
        }
    }
}
