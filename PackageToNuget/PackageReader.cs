using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget
{
    public class PackageReader
    {
        private readonly string path;

        public PackageReader(string path)
        {
            this.path = path;
        }

        public PackageDefinition ReadDefinition()
        {
            using (var zipFile = new ZipFile(path))
            {
                return ReadDefinition(zipFile);
            }
        }

        internal static PackageDefinition ReadDefinition(ZipFile zipFile)
        {
            var entry = FindPackageXml(zipFile);
            using (var stream = zipFile.GetInputStream(entry))
            {
                return PackageDefinition.Load(stream);
            }
        }

        public static ZipEntry FindPackageXml(ZipFile zipFile)
        {
            foreach(ZipEntry entry in zipFile)
                if (entry.Name.EndsWith("package.xml", StringComparison.OrdinalIgnoreCase))
                    return entry;
            return null;
        }
    }
}
