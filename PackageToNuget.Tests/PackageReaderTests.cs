using System;
using System.IO;
using NUnit.Framework;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class PackageReaderTests
    {
        [Test]
        [TestCase("imagegen_2.9.0.zip", "imagegen.xml")]
        [TestCase("doctypemixins_2.0.zip", "doctypemixins.xml")]
        [TestCase("google_maps_for_umbraco_2.1.0.zip", "google_maps_for_umbraco.xml")]
        public void ReadPackage_ReturnsPackageDefinition(string zipFile, string expectedPackageXmlFile)
        {
            var path = TestData.GetTestFilePath(zipFile);
            var expected = PackageDefinition.Load(TestData.GetTestFilePath(expectedPackageXmlFile));

            var packageReader = new PackageReader(path);
            var definition = packageReader.ReadDefinition();

            Assert.AreEqual(expected, definition);
        }
    }
}
