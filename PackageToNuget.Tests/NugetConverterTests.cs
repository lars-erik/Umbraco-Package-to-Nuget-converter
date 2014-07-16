using System;
using System.IO;
using NUnit.Framework;

namespace PackageToNuget.Tests
{
    [TestFixture]
    public class NugetConverterTests
    {
        [Test]
        public void Convert_AzureStorage_CreatesNewFolderWithContent()
        {
            ConvertAndAssertAzureStoragePackage(outputPath =>
                Assert.IsTrue(File.Exists(outputPath + @"\config\FileSystemProvider-sample.config"))
                );
        }

        [Test]
        public void Convert_AzureStorage_CreatesNewFolderWithPackageStructure()
        {
            ConvertAndAssertAzureStoragePackage(outputPath =>
            {
                Assert.IsTrue(File.Exists(outputPath + @"\config\FileSystemProvider"));
                Assert.IsTrue(File.Exists(outputPath + @"\bin\idseefeld.de.UmbracoAzure.dll"));
            });
        }

        [Test]
        public void Convert_AzureStorage_DownloadsDependencies()
        {
            ConvertAndAssertAzureStoragePackage(outputPath =>
            {
                Assert.IsTrue(File.Exists(outputPath + @"\bin\WindowsAzure.Storage.dll"));
                Assert.IsTrue(File.Exists(outputPath + @"\bin\Microsoft.Data.Edm.dll"));
            });
        }

        private void ConvertAndAssertAzureStoragePackage(Action<string> asserts)
        {
            var path = TestData.GetTestFilePath("Umbraco.Azure.Storage.1.1.0.0.nupkg");
            var converter = new NugetConverter(path);
            var outputPath = CreateTestDirectory();
            try
            {
                converter.GeneratePackage(outputPath);
                asserts(outputPath);
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
