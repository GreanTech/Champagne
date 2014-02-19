using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grean.Champagne.UnitTests
{
    public class Examples
    {
        [Fact]
        public void ReplaceNumberInSequence()
        {
            var seq = new[] { 4, 2, 42, 1337, 7 };
            var actual = seq.Replace(9, i => i == 2);
            Assert.Equal(new[] { 4, 9, 42, 1337, 7 }, actual);
        }
    }
}
