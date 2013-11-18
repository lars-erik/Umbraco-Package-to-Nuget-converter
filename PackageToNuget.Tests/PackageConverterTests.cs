using System;
using System.IO;
using NUnit.Framework;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class PackageConverterTests
    {
        [Test]
        public void Convert_CreatesNewZipWithNugetStructure()
        {
            var path = TestData.GetTestFilePath("doctypemixins_2.0.zip");
            var converter = new PackageConverter(path);
            var guid = Guid.NewGuid();
            var outputPath = TestData.GetTestFilePath(guid.ToString("N"));
            Directory.CreateDirectory(outputPath);
            converter.GenerateNuget(outputPath);
            Assert.IsTrue(File.Exists(outputPath + @"\content\app_plugins\doctypemixins\images\doctypemixin.png"));
            Assert.IsTrue(File.Exists(outputPath + @"\lib\our.umbraco.doctypemixins.dll"));
            Directory.Delete(outputPath, true);
        }

        [Test]
        public void Convert_AddsNuspecFile()
        {
            Assert.Fail();
        }
    }
}
