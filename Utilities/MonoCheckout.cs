using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceIO;

namespace Utilities
{
    public class MonoCheckout
    {
        public readonly NPath Root;

        public MonoCheckout(NPath monoCheckoutRoot)
        {
            Root = monoCheckoutRoot;
        }

        public NPath ReferenceSourcesFilePathFor(NPath classLibraryAssemblyDirectory, string profileName)
        {
            var classLibNameForSourcesFile = SourcesUtils.ClassLibSourcesFileNameOnDiskFromParentDirectory(classLibraryAssemblyDirectory);
            var normalNamingScheme = $"{profileName}_{classLibNameForSourcesFile}.dll.sources";

            var normalNamingPath = classLibraryAssemblyDirectory.Combine(normalNamingScheme);
            if (normalNamingPath.FileExists())
                return normalNamingPath;

            var alternateNamingPath = classLibraryAssemblyDirectory.Combine($"{classLibNameForSourcesFile}.dll.sources");
            if (alternateNamingPath.FileExists())
                return alternateNamingPath;

            return normalNamingPath;
        }

        public NPath OutputProfileSourcesFilePathFor(NPath classLibraryAssemblyDirectory, string profileName)
        {
            var classLibNameForSourcesFile = SourcesUtils.ClassLibSourcesFileNameOnDiskFromParentDirectory(classLibraryAssemblyDirectory);
            return classLibraryAssemblyDirectory.Combine($"{profileName}_{classLibNameForSourcesFile}.dll.sources");
        }
    }
}
