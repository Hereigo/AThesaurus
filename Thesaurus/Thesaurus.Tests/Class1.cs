using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Thesaurus.Tests
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Sum(2, 2));
        }


        private int Sum(int v1, int v2)
        {
            return v1 + v2;
        }
    }
}
