using System;
using System.IO;
using NUnit.Framework;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class PackageConverterTests
    {
        [Test]
        public void Convert_DocTypeMixins_CreatesNewZipWithNugetStructure()
        {
            var path = TestData.GetTestFilePath("doctypemixins_2.0.zip");
            var converter = new PackageConverter(path);
            var outputPath = CreateTestDirectory();
            try
            {
                converter.GenerateNuget(outputPath);
                Assert.IsTrue(File.Exists(outputPath + @"\content\app_plugins\doctypemixins\images\doctypemixin.png"));
                Assert.IsTrue(File.Exists(outputPath + @"\lib\our.umbraco.doctypemixins.dll"));
            }
            finally
            {
                Directory.Delete(outputPath, true);
            }
        }

        [Test]
        public void Convert_ImageGen_CreatesNewZipWithNugetStructure()
        {
            var path = TestData.GetTestFilePath("imagegen_2.9.0.zip");
            var converter = new PackageConverter(path);
            var outputPath = CreateTestDirectory();
            try
            {
                converter.GenerateNuget(outputPath);
                Assert.IsTrue(File.Exists(outputPath + @"\content\imagegen.ashx"));
                Assert.IsTrue(File.Exists(outputPath + @"\lib\imagegen.dll"));
            }
            finally
            {
                Directory.Delete(outputPath, true);
            }
        }

        [Test]
        public void Convert_AddsNuspecFile()
        {
            string expected;
            using (var stream = File.OpenRead(TestData.GetTestFilePath("doctypemixins.nuspec")))
            {
                var reader = new StreamReader(stream);
                expected = reader.ReadToEnd();
            }

            var path = TestData.GetTestFilePath("doctypemixins_2.0.zip");
            var converter = new PackageConverter(path);
            var outputPath = CreateTestDirectory();

            try
            {
                converter.GenerateNuget(outputPath);

                using (var stream = File.OpenRead(Path.Combine(outputPath, "doctypemixins.nuspec")))
                {
                    var reader = new StreamReader(stream);
                    Assert.AreEqual(expected, reader.ReadToEnd());
                }
            }
            finally
            {
                Directory.Delete(outputPath, true);
            }
        }

        private static string CreateTestDirectory()
        {
            var guid = Guid.NewGuid();
            var outputPath = TestData.GetTestFilePath(guid.ToString("N"));
            Directory.CreateDirectory(outputPath);
            return outputPath;
        }
    }
}
