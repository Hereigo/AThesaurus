using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Thesaurus.Tests
{
    public class Thesaurus_Tests
    {
        // TODO:
        // Create MOCKs to emulate DB different from sqlite.

        Thesaurus thesaurus = new Thesaurus();

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        [InlineData("!%$@!&#^_+", "_)&)(^&*(%$", "(*^&%*&%#&$@")]
        [InlineData("", "", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void AddSynonyms_MustInsertAnyStrings_WithNoException(string s1, string s2, string s3)
        {
            IEnumerable<string> strings = new List<string> { s1, s2, s3 };

            var excep = Record.Exception(() => thesaurus.AddSynonyms(strings));
            Assert.Null(excep);
        }

        // TODO :
        // check for increment of db records after insert new
        // and no increment after update!

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("(*^$*%@")]
        [InlineData("woman plane")]
        public void GetSynonyms_ForIncorrect_IsEmpty(string input)
        {
            IEnumerable<string> resultForIncorrect = new List<string>();

            var realResult = thesaurus.GetSynonyms(input);

            Assert.Empty(realResult);
        }

        [Theory]
        [InlineData("city ")]
        [InlineData(" woman")]
        [InlineData(" plane ")]
        public void GetSynonyms_Correct_IsNotEmpty(string input)
        {
            IEnumerable<string> resultForIncorrect = new List<string>();

            var realResult = thesaurus.GetSynonyms(input);

            Assert.NotEmpty(realResult);
        }
    }
}
