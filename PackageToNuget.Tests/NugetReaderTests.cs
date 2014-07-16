using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PackageToNuget.NugetDefinitions;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class NugetReaderTests
    {
        [Test]
        [TestCase("Umbraco.Azure.Storage.1.1.0.0.nupkg", "Umbraco.Azure.Storage.nuspec")]
        //[TestCase("doctypemixins_2.0.zip", "doctypemixins.xml")]
        //[TestCase("google_maps_for_umbraco_2.1.0.zip", "google_maps_for_umbraco.xml")]
        public void ReadPackage_ReturnsPackageDefinition(string nuPkgFile, string expectedNuSpecFile)
        {
            var path = TestData.GetTestFilePath(nuPkgFile);
            var expected = NuSpec.Load(TestData.GetTestFilePath(expectedNuSpecFile));

            var nuspecReader = new NugetReader(path);
            var definition = nuspecReader.ReadDefinition();

            Assert.AreEqual(expected, definition);
        }
    }
}
