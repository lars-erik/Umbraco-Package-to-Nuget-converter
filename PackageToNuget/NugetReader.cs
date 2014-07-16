using System;
using ICSharpCode.SharpZipLib.Zip;
using PackageToNuget.NugetDefinitions;

namespace PackageToNuget
{
    public class NugetReader
    {
        private readonly string path;

        public NugetReader(string path)
        {
            this.path = path;
        }

        public NuSpec ReadDefinition()
        {
            using (var zipFile = new ZipFile(path))
            {
                return ReadNuspec(zipFile);
            }
        }

        public static NuSpec ReadNuspec(ZipFile zipFile)
        {
            var entry = FindPackageNuspec(zipFile);
            using (var stream = zipFile.GetInputStream(entry))
            {
                return NuSpec.Load(stream);
            }
        }

        public static ZipEntry FindPackageNuspec(ZipFile zipFile)
        {
            foreach (ZipEntry entry in zipFile)
                if (entry.Name.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase))
                    return entry;
            return null;
        }
    }
}
