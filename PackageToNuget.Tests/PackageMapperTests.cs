using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NUnit.Framework;
using PackageToNuget.NugetDefinitions;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class PackageMapperTests
    {
        [Test]
        public void Map_CreatesNuspecDefinitionFromInfo()
        {
            var reader = new PackageReader(TestData.GetTestFilePath("doctypemixins_2.0.zip"));
            var definition = reader.ReadDefinition();

            var mapper = new PackageMapper();
            var nuspec = mapper.Map(definition);

            var expected = new NuSpec
            {
                Metadata = new Metadata
                {
                    Id = "DocTypeMixins",
                    Version = "2.0",
                    LicenseUrl = "http://www.opensource.org/licenses/mit-license.php",
                    ProjectUrl = "http://our.umbraco.org/projects/backoffice-extensions/doctypemixins",
                    Authors = "Pete Duncanson, Matt Brailsford"
                }
            };

            var serializer = new XmlSerializer(typeof (NuSpec));
            var builderA = new StringBuilder();
            var builderB = new StringBuilder();
            var writer = new StringWriter(builderA);
            serializer.Serialize(writer, expected);
            writer.Flush();
            writer = new StringWriter(builderB);
            serializer.Serialize(writer, nuspec);
            writer.Flush();

            Assert.AreEqual(builderA.ToString(), builderB.ToString());
        }
    }
}
