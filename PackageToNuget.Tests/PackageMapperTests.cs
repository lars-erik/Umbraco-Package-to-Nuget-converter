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
        public void Map_DocTypeMixins_IsValidNuspec()
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
                    Authors = "Pete Duncanson, Matt Brailsford",
                    Description = "DocTypeMixins"
                }
            };

            SerializeAndAssert(expected, nuspec);
        }

        [Test]
        public void Map_GoogleMapsForUmbraco_IsValidNuspec()
        {
            var reader = new PackageReader(TestData.GetTestFilePath("Google_Maps_for_Umbraco_2.1.0.zip"));
            var definition = reader.ReadDefinition();

            var mapper = new PackageMapper();
            var nuspec = mapper.Map(definition);

            var expected = new NuSpec
            {
                Metadata = new Metadata
                {
                    Id = "Google.Maps.for.Umbraco",
                    Version = "2.1.0",
                    LicenseUrl = "http://opensource.org/licenses/MIT",
                    ProjectUrl = "http://our.umbraco.org/projects/backoffice-extensions/google-maps-datatype",
                    Authors = "Darren Ferguson, Lee Kelleher, Douglas Ludlow",
                    Description = @"The Google Maps Datatype for Umbraco allows you to specify a latitude/longitude point to be saved in an Umbraco document.

This point is chosen using a Google map."
                }
            };

            SerializeAndAssert(expected, nuspec);
        }

        private static void SerializeAndAssert(NuSpec expected, NuSpec actual)
        {
            var serializer = new XmlSerializer(typeof (NuSpec));
            var expectedBuilder = new StringBuilder();
            var actualBuilder = new StringBuilder();
            var writer = new StringWriter(expectedBuilder);
            serializer.Serialize(writer, expected);
            writer.Flush();
            writer = new StringWriter(actualBuilder);
            serializer.Serialize(writer, actual);
            writer.Flush();

            Console.WriteLine(actualBuilder.ToString());

            Assert.AreEqual(expectedBuilder.ToString(), actualBuilder.ToString());
        }
    }
}
