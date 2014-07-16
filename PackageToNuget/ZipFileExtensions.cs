using System.IO;
using ICSharpCode.SharpZipLib.Zip;

static internal class ZipFileExtensions
{
    public static void Write(this ZipFile zipFile, ZipEntry origEntry, string physicalPath)
    {
        using (var stream = zipFile.GetInputStream(origEntry))
        {
            using (var file = File.Open(physicalPath, FileMode.Create, FileAccess.Write))
            {
                var buffer = new byte[1024];
                int count = 0;
                while ((count = stream.Read(buffer, 0, 1024)) > 0)
                {
                    file.Write(buffer, 0, count);
                }
                file.Flush(true);
            }
        }
    }
}