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
    public class NugetConverter
    {
        private const StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;

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
                var packageDef = new PackageDefinition(); // TODO: Map meta

                var zipEntries = origZip
                    .Cast<ZipEntry>()
                    .AsQueryable();

                var contentFiles = zipEntries
                    .Where(IsContentFile);

                foreach (var file in contentFiles)
                {
                    var newPath = GetNewPath(file.Name);
                    var packageFile = new PackageFile
                    {
                        Guid = file.Name,
                        OrgName = Path.GetFileName(newPath),
                        OrgPath = Path.GetDirectoryName(newPath)
                    };
                    packageDef.Files.Add(packageFile);

                    var physPath = Path.Combine(outputPath, packageFile.OrgName);
                    origZip.Write(file, physPath);
                }

                var binaryFiles = zipEntries
                    .Where(IsInLibDirectory);


            }
        }

        private static string GetNewPath(string path)
        {
            if (path.StartsWith("content/", IgnoreCase))
                path = path.Substring(8);
            if (path.StartsWith("lib/", IgnoreCase))
                path = "bin/" + path.Substring(4);
            path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }

        private static bool IsContentFile(ZipEntry e)
        {
            return 
                e.IsFile &&
                e.Name.StartsWith("content/", IgnoreCase);
        }

        private static bool IsInLibDirectory(ZipEntry e)
        {
            return e.Name.StartsWith("lib/", IgnoreCase);
        }
    }
}
