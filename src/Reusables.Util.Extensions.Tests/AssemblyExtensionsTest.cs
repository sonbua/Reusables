using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class AssemblyExtensionsTest
    {
        [Fact]
        public void GetLoadableTypes_NullAssembly_ThrowsException()
        {
            // arrange

            // act
            Action nullAssembly = () => ((Assembly) null).GetLoadableTypes();

            // assert
            Assert.Throws<ArgumentNullException>(nullAssembly);
        }

        [Fact]
        public void GetLoadableTypes_ThisAssembly_ReturnsAllTypesWithTestSuffix()
        {
            // arrange
            var thisAssembly = Assembly.GetExecutingAssembly();

            // act
            var publicLoadableTypes = thisAssembly.GetLoadableTypes()
                                                  .Where(t => t.IsPublic);

            // assert
            Assert.True(publicLoadableTypes.All(t => t.Name.EndsWith("Test")));
        }
    }
}
