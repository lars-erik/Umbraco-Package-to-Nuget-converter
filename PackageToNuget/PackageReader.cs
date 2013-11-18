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
            var packagePath = "package.xml";
            var root = FindRoot(zipFile);
            packagePath = ZipEntry.CleanName(Path.Combine(root, packagePath));

            var entry = zipFile.GetEntry(packagePath);
            using (var stream = zipFile.GetInputStream(entry))
            {
                return PackageDefinition.Load(stream);
            }
        }

        public static string FindRoot(ZipFile zipFile)
        {
            var root = "";
            var firstEntry = zipFile[0];
            Guid rootFolderId;
            if (Guid.TryParse(firstEntry.Name.TrimEnd('/'), out rootFolderId))
                root = rootFolderId.ToString("D");
            return root;
        }
    }
}
