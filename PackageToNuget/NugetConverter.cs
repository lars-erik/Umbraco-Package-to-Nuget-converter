using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace PackageToNuget
{
    public class NugetConverter
    {
        private readonly string path;
        private string outputPath;
        private ZipFile origZip;

        public NugetConverter(string path)
        {
            this.path = path;
        }

        public void GeneratePackage(string outputPath)
        {
            this.outputPath = outputPath;
            using (origZip = new ZipFile(path))
            {
                var definition = NugetReader.ReadNuspec(origZip);
            }
        }
    }
}
