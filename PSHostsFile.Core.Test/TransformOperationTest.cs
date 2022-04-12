using System.Linq;
using System.Text;
using Xunit;

namespace PSHostsFile.Core.Test
{
    [Collection("Sequential")]
    public class TransformOperationTest : ReadWriteScenario
    {
        [Fact]
        public void preserves_original_encoding_UTF8()
        {
            preserves_original_encoding(Encoding.UTF8);
        }

        [Fact]
        public void preserves_original_encoding_UTF32()
        {
            preserves_original_encoding(Encoding.UTF32);
        }

        public void preserves_original_encoding(Encoding encoding)
        {
            string expectedContents = string.Join("", Enumerable.Range(0, 2048)
                                                      .Select(i => (char)i)
                                                      .Where(c => c != '\n' && c != '\r')
                                                      .Select(c => c.ToString() + "\r\n")
                                                      .ToArray());

            string filename = GetFileWithContents(expectedContents, encoding);

            TransformOperation.TransformFile(filename, lines => lines);

            Encoding encodingUsed;
            string fileContents = ReadFileContents(filename, out encodingUsed);

            Assert.Equal(encodingUsed, encoding);
            Assert.Equal(fileContents, expectedContents);
        }
    }
}
