using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Thesaurus.Tests
{
    public class Tests
    {
        Thesaurus thesaurus = new Thesaurus();

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        [InlineData("!%$@!&#^_+", "_)&)(^&*(%$", "(*^&%*&%#&$@")]
        [InlineData("", "", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void AddSynonyms_WithoutExceptions(string s1, string s2, string s3)
        {
            IEnumerable<string> strings = new List<string> { s1, s2, s3 }; 

            var excep = Record.Exception(() => thesaurus.AddSynonyms(strings));
            Assert.Null(excep);
        }
    }
}
