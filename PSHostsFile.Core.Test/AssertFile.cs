using System.IO;
using Xunit;

namespace PSHostsFile.Core.Test
{
    class AssertFile
    {
        public static void MatchesIgnoringNewlines(string hostsFile, string expectedString)
        {
            string a = File.ReadAllText(hostsFile);
            string b = expectedString;
            a = a.Replace("\r\n", "\n");
            b = b.Replace("\r\n", "\n");
            Assert.True(a == b);
        }
    }
}
