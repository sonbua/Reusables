using System;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ObjectExtensions_GetOrder_Test
    {
        private class ClassWithoutOrderAttribute
        {
        }

        [Order(3)]
        private class ClassWithOrderValueOfThreeAttribute
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
            var order = instance.GetOrder();

            // assert
            Assert.Equal(0, order);
        }

        [Fact]
        public void GivenClassWithOrderValueOfThreeAttribute_ReturnsThree()
        {
            // arrange
            var instance = new ClassWithOrderValueOfThreeAttribute();

            // act
            var order = instance.GetOrder();

            // assert
            Assert.Equal(3, order);
        }

        [Fact]
        public void GivenDerivedInstanceButAppearsToBeParent_ReturnsParentsOrder()
        {
            // arrange
            ClassParent instance = new ClassDerived();

            // act
            var order = instance.GetOrder();

            // assert
            Assert.Equal(1, order);
        }
    }
}