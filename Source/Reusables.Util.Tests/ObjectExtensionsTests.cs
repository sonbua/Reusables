using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ToDictionary_NullInstance_ThrowsException()
        {
            // arrange
            object instance = null;

            // act
            Action nullInstance = () => instance.ToDictionary();

            // assert
            Assert.Throws<ArgumentNullException>("instance", nullInstance);
        }

        [Theory]
        [AutoData]
        public void ToDictionary_ClassWithPublicProperty_ReturnsCorrectNumberOfMembers(ClassWithPublicProperty instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(1, memberDictionary.Count);
        }

        [Theory]
        [AutoData]
        public void ToDictionary_ClassWithPublicField_ReturnsCorrectNumberOfMembers(ClassWithPublicField instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(1, memberDictionary.Count);
        }

        [Theory]
        [AutoData]
        public void ToDictionary_ClassWithPrivateField_ReturnsCorrectNumberOfMembers(ClassWithPrivateField instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(0, memberDictionary.Count);
        }

        [Theory]
        [AutoData]
        public void ToDictionary_ClassWithPublicStaticField_ThrowsException(ClassWithPublicStaticField instance)
        {
            // arrange

            // act
            Action accessClassWithPublicStaticField = () => instance.ToDictionary();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(accessClassWithPublicStaticField);
        }

        public class ClassWithPublicProperty
        {
            public string Prop1 { get; set; }
        }

        public class ClassWithPublicField
        {
            public string Field1 = string.Empty;
        }

        public class ClassWithPrivateField
        {
            private string _field1 = string.Empty;
        }

        public class ClassWithPublicStaticField
        {
            public static string Field1 = string.Empty;
        }
    }
}
