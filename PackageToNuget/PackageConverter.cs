using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using PackageToNuget.NugetDefinitions;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget
{
    public class PackageConverter
    {
        private const StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;
        private readonly string path;
        private string outputPath;
        private ZipFile origZip;
        private string root;

        public PackageConverter(string path)
        {
            this.path = path;
        }

        public void GenerateNuget(string outputPath)
        {
            this.outputPath = outputPath;
            using (origZip = new ZipFile(path))
            {
                var definition = PackageReader.ReadDefinition(origZip);
                root = PackageReader.FindRoot(origZip);
                foreach (var packageFile in definition.Files)
                {
                    AddFile(packageFile);
                }
                AddNuSpec(definition);
            }
        }

        private void AddNuSpec(PackageDefinition definition)
        {
            var mapper = new PackageMapper();
            var nuspec = mapper.Map(definition);
            using (var file = File.Open(
                Path.Combine(this.outputPath, definition.Info.Package.Name + ".nuspec"),
                FileMode.Create, FileAccess.Write)
            )
            {
                SerializeNuSpec(nuspec, file);
            }
        }

        private void AddFile(PackageFile packageFile)
        {
            var newPath = GetNewPath(packageFile);
            var relativeDirectoryPath = Path.GetDirectoryName(newPath);
            var physicalDirectoryPath = Path.Combine(outputPath, relativeDirectoryPath);
            if (!Directory.Exists(physicalDirectoryPath))
                Directory.CreateDirectory(physicalDirectoryPath);
            var physicalPath = Path.Combine(outputPath, newPath);
            var origEntry = origZip.GetEntry(packageFile.Guid) ?? origZip.GetEntry(root + "/" + packageFile.Guid);
            using (var stream = origZip.GetInputStream(origEntry))
            {
                using (var file = File.Open(physicalPath, FileMode.Create, FileAccess.Write))
                {
                    var buffer = new byte[1024];
                    int count = 0;
                    while((count = stream.Read(buffer, 0, 1024)) > 0)
                    {
                        file.Write(buffer, 0, count);
                    }
                    file.Flush(true);
                }
            }
        }

        private static string GetNewPath(PackageFile packageFile)
        {
            if (packageFile.OrgPath.StartsWith("/bin", IgnoreCase))
                return "lib/" + packageFile.OrgName;
            return "content" + packageFile.OrgPath + "/" + packageFile.OrgName;
        }

        public static string SerializeNuSpec(NuSpec nuspec)
        {
            using (var stream = new MemoryStream())
            {
                SerializeNuSpec(nuspec, stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private static void SerializeNuSpec(NuSpec nuspec, Stream stream)
        {
            var settings = new XmlWriterSettings {Indent = true, Encoding = Encoding.UTF8};
            var serializer = new XmlSerializer(typeof (NuSpec), new XmlRootAttribute("package") {Namespace = ""});
            var writer = XmlWriter.Create(stream, settings);
            serializer.Serialize(writer, nuspec, new XmlSerializerNamespaces(new[] {new XmlQualifiedName("")}));
            writer.Flush();
        }
    }
}
