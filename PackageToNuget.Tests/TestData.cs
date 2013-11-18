using System;
using System.IO;

namespace PackageToNuget.Tests
{
    static internal class TestData
    {
        private static readonly string testDataPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\TestData");

        public static string GetTestFilePath(string zipFile)
        {
            var path = Path.Combine(testDataPath, zipFile);
            return path;
        }
    }
}