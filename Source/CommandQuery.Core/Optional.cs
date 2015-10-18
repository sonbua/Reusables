using System;
using System.Collections.Generic;

namespace CommandQuery.Core
{
    public class Optional<T>
    {
        private readonly T _value;

        public bool HasValue { get; private set; }

        public T Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }

                throw new InvalidOperationException("Value has not been set.");
            }
        }

        public Optional(T value)
        {
            _value = value;
            HasValue = true;
        }

        public static explicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }

        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        public override bool Equals(object obj)
        {
            var optionalObj = obj as Optional<T>;

            return optionalObj != null && Equals(optionalObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value)*397) ^ HasValue.GetHashCode();
            }
        }

        public bool Equals(Optional<T> other)
        {
            if (HasValue && other.HasValue)
            {
                return Equals(_value, other._value);
            }

            return HasValue == other.HasValue;
        }
    }
}