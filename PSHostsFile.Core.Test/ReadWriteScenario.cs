using System.IO;
using System.Text;

namespace PSHostsFile.Core.Test
{
    public class ReadWriteScenario
    {
        public static string ReadFileContents(string filename, out Encoding encodingUsed)
        {
            string fileContents;

            using (var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var streamReader = new StreamReader(stream);
                fileContents = streamReader.ReadToEnd();
                encodingUsed = streamReader.CurrentEncoding;
            }
            return fileContents;
        }

        public static string GetFileWithContents(string contents, Encoding encoding)
        {
            var filename = Path.Combine(Path.GetTempPath(), "PSHostsFileTest.temp.hosts");

            if (File.Exists(filename))
                File.Delete(filename);

            File.WriteAllText(filename, contents, encoding);

            return filename;
        }
    }
}