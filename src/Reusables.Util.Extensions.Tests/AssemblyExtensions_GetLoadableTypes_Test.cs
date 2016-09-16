using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class AssemblyExtensions_GetLoadableTypes_Test
    {
        [Fact]
        public void GivenNullAssembly_ThrowsException()
        {
            // arrange

            // act
            Action nullAssembly = () => ((Assembly) null).GetLoadableTypes();

            // assert
            Assert.Throws<ArgumentNullException>(nullAssembly);
        }

        [Fact]
        public void GivenThisAssembly_ReturnsAllTypesWithTestSuffix()
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