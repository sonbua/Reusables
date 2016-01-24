using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ObjectExtensionsTest
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

        [Fact]
        public void GetOrder_NullInstance_ThrowsException()
        {
            // arrange

            // act
            Action nullInstance = () => { ((object) null).GetOrder(); };

            // assert
            Assert.Throws<ArgumentNullException>("instance", nullInstance);
        }

        [Fact]
        public void GetOrder_ClassWithoutOrderAttribute_ReturnsZero()
        {
            // arrange
            var instance = new ClassWithoutOrderAttribute();

            // act
            var order = instance.GetOrder();

            // assert
            Assert.Equal(0, order);
        }

        [Fact]
        public void GetOrder_ClassWithOrderValueOfThreeAttribute_ReturnsThree()
        {
            // arrange
            var instance = new ClassWithOrderValueOfThreeAttribute();

            // act
            var order = instance.GetOrder();

            // assert
            Assert.Equal(3, order);
        }

        [Fact]
        public void GetOrder_DerivedInstanceButAppearsToBeParent_ReturnsParentsOrder()
        {
            // arrange
            ClassParent instance = new ClassDerived();

            // act
            var order = instance.GetOrder();

            // assert
            Assert.Equal(1, order);
        }

        [Fact]
        public void GetRuntimeOrder_NullInstance_ThrowsException()
        {
            // arrange

            // act
            Action nullInstance = () => { ((object) null).GetOrder(); };

            // assert
            Assert.Throws<ArgumentNullException>("instance", nullInstance);
        }

        [Fact]
        public void GetRuntimeOrder_ClassWithoutOrderAttribute_ReturnsZero()
        {
            // arrange
            var instance = new ClassWithoutOrderAttribute();

            // act
            var order = instance.GetRuntimeOrder();

            // assert
            Assert.Equal(0, order);
        }

        [Fact]
        public void GetRuntimeOrder_DerivedInstanceButAppearsToBeParent_ReturnsDerivedsOrder()
        {
            // arrange
            ClassParent instance = new ClassDerived();

            // act
            var order = instance.GetRuntimeOrder();

            // assert
            Assert.Equal(2, order);
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

        public class ClassWithoutOrderAttribute
        {
        }

        [Order(3)]
        public class ClassWithOrderValueOfThreeAttribute
        {
        }

        [Order(1)]
        public class ClassParent
        {
        }

        [Order(2)]
        public class ClassDerived : ClassParent
        {
        }
    }
}
