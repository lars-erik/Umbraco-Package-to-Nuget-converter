using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using PackageToNuget.NugetDefinitions;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class DefinitionSerializationTests
    {
        [Test]
        public void Deserialize_CreatesFullDto()
        {
            var path = TestData.GetTestFilePath("imagegen.xml");
            var def = PackageDefinition.Load(path);

            Assert.AreEqual(4, def.Files.Count);
            var firstFile = def.Files.First();
            Assert.AreEqual("imagegen.dll", firstFile.Guid);
            Assert.AreEqual("/bin", firstFile.OrgPath);
            Assert.AreEqual("imagegen.dll", firstFile.OrgName);

            Assert.IsNotNull(def.Info.Package);
            Assert.AreEqual("ImageGen", def.Info.Package.Name);
            Assert.AreEqual("2.9.0", def.Info.Package.Version);
            Assert.AreEqual("http://www.percipientstudios.com/imagegen/license.aspx", def.Info.Package.License.Url);
            Assert.AreEqual("Proprietary License", def.Info.Package.License.Name);
            Assert.AreEqual("http://www.percipientstudios.com", def.Info.Package.Url);
            Assert.AreEqual("Douglas Robar", def.Info.Author.Name);
        }

        [Test]
        public void PackageConverter_SerializeNuSpec_WritesNuSpecFile()
        {
            string expected;
            using (var stream = File.OpenRead(TestData.GetTestFilePath("doctypemixins.nuspec")))
            {
                var reader = new StreamReader(stream);
                expected = reader.ReadToEnd();
            }

            var nuspec = new NuSpec
            {
                Metadata = new Metadata
                {
                    Id = "DocTypeMixins",
                    Version = "2.0",
                    LicenseUrl = "http://www.opensource.org/licenses/mit-license.php",
                    ProjectUrl = "http://our.umbraco.org/projects/backoffice-extensions/doctypemixins",
                    Authors = "Pete Duncanson, Matt Brailsford",
                    Description = "DocTypeMixins"
                }
            };

            var serialized = PackageConverter.SerializeNuSpec(nuspec);
            Assert.AreEqual(expected, serialized);
        }
    }
}
