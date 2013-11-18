using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
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
    }
}
