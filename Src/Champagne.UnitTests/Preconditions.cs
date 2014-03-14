using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grean.Champagne.UnitTests
{
    public class Preconditions
    {
        [Fact]
        public void ReplaceWithNullFuncThrows()
        {
            var seq = new List<object> { new object(), new object() };
            var dummyValue = new object();
            Assert.Throws<ArgumentNullException>(
                () => seq.Replace(
                    dummyValue,
                    (Func<object, bool>)null).ToList());
        }
    }
}
