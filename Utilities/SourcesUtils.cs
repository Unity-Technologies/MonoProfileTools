using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceIO;

namespace Utilities
{
    public class SourcesUtils
    {
        public static string ClassLibSourcesFileNameOnDiskFromParentDirectory(NPath directory)
        {
            if (directory.FileName == "System.ComponentModel.Composition.4.5")
                return "System.ComponentModel.Composition";

            if (directory.FileName == "System.XML")
                return "System.Xml";

            return directory.FileName;
        }
    }
}
