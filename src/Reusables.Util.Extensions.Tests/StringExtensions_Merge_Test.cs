using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_Merge_Test
    {
        [Theory]
        [ClassData(typeof(People))]
        public void Merge_ReturnsCorrectFormattedString(string template, object data, char fieldStartDelimiter, char fieldEndDelimiter, string expected)
        {
            // arrange

            // act
            var merged = template.Merge(data, fieldStartDelimiter, fieldEndDelimiter);

            // assert
            Assert.Equal(expected, merged);
        }

        private class People : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                             {
                                 "{Name} is {Age} years old and works as {Profession}",
                                 new {Name = "John Doe", Age = 30, Profession = "Developer"},
                                 '{', '}',
                                 "John Doe is 30 years old and works as Developer"
                             };

                yield return new object[]
                             {
                                 "{Name} is {Age} years old and works as {Profession}",
                                 new {Name = "John Doe", Age = 30, Job = "Other"},
                                 '{', '}',
                                 "John Doe is 30 years old and works as {Profession}"
                             };

                yield return new object[]
                             {
                                 "<Name> is <Age> years old and works as <Profession>",
                                 new {Name = "John Doe", Age = 30, Profession = "Developer"},
                                 '<', '>',
                                 "John Doe is 30 years old and works as Developer"
                             };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}