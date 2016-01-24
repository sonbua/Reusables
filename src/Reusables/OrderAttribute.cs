using System;

namespace Reusables
{
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
