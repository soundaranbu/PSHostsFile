using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace PSHostsFile.Core.Test
{
    [Collection("Sequential")]
    public class RemoveTest : ReadWriteScenario
    {
        [Fact]
        public void can_remove_an_entry()
        {
            string filename = GetFileWithContents(@"
127.0.0.1           localhost
10.90.82.100        somehost

", Encoding.UTF8);

            var sut = new Remove();

            sut.RemoveFromFile("somehost", filename);

            AssertFile.MatchesIgnoringNewlines(filename, @"
127.0.0.1           localhost

");
        }

        [Fact]
        public void can_remove_by_regex()
        {
            string filename = GetFileWithContents(@"
11.90.82.100        ladeda
127.0.0.1           localhost
10.90.82.100        somehost
11.90.82.100        anotherserver

", Encoding.UTF8);

            var sut = new Remove();

            sut.RemoveFromFile(new Regex(".*host"), filename);

            AssertFile.MatchesIgnoringNewlines(filename, @"
11.90.82.100        ladeda
11.90.82.100        anotherserver

");
        }
    }
}
