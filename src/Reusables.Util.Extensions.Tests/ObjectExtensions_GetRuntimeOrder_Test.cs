using System;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ObjectExtensions_GetRuntimeOrder_Test
    {
        private class ClassWithoutOrderAttribute
        {
        }

        [Order(1)]
        private class ClassParent
        {
        }

        [Order(2)]
        private class ClassDerived : ClassParent
        {
        }

        [Fact]
        public void GivenNullInstance_ThrowsException()
        {
            // arrange

            // act
            Action nullInstance = () => { ((object) null).GetOrder(); };

            // assert
            Assert.Throws<ArgumentNullException>("instance", nullInstance);
        }

        [Fact]
        public void GivenClassWithoutOrderAttribute_ReturnsZero()
        {
            // arrange
            var instance = new ClassWithoutOrderAttribute();

            // act
            var order = instance.GetRuntimeOrder();

            // assert
            Assert.Equal(0, order);
        }

        [Fact]
        public void GivenDerivedInstanceButAppearsToBeParent_ReturnsDerivedsOrder()
        {
            // arrange
            ClassParent instance = new ClassDerived();

            // act
            var order = instance.GetRuntimeOrder();

            // assert
            Assert.Equal(2, order);
        }
    }
}