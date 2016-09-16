using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ObjectExtensions_ToDictionary_Test
    {
        private class ClassWithPublicProperty
        {
            public string Prop1 { get; set; }
        }

        private class ClassWithPublicField
        {
            public string Field1 = string.Empty;
        }

        private class ClassWithPrivateField
        {
            private string _field1 = string.Empty;
        }

        private class ClassWithPublicStaticField
        {
            public static string Field1 = string.Empty;
        }

        [Fact]
        public void GivenNullInstance_ThrowsException()
        {
            // arrange

            // act
            Action nullInstance = () => ((object) null).ToDictionary();

            // assert
            Assert.Throws<ArgumentNullException>("instance", nullInstance);
        }

        [Theory, AutoData]
        private void GivenClassWithPublicProperty_ReturnsCorrectNumberOfMembers(ClassWithPublicProperty instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(1, memberDictionary.Count);
        }

        [Theory, AutoData]
        private void GivenClassWithPublicField_ReturnsCorrectNumberOfMembers(ClassWithPublicField instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(1, memberDictionary.Count);
        }

        [Theory, AutoData]
        private void GivenClassWithPrivateField_ReturnsCorrectNumberOfMembers(ClassWithPrivateField instance)
        {
            // arrange

            // act
            var memberDictionary = instance.ToDictionary();

            // assert
            Assert.Equal(0, memberDictionary.Count);
        }

        [Theory, AutoData]
        private void GivenClassWithPublicStaticField_ThrowsException(ClassWithPublicStaticField instance)
        {
            // arrange

            // act
            Action accessClassWithPublicStaticField = () => instance.ToDictionary();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(accessClassWithPublicStaticField);
        }
    }
}