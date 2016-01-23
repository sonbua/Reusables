namespace Reusables
{
    public struct Nothing
    {
        public bool Equals(Nothing other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Nothing;
        }

        public override int GetHashCode()
        {
            // any prime number
            return 397;
        }

        public static bool operator ==(Nothing first, Nothing second)
        {
            return true;
        }

        public static bool operator !=(Nothing first, Nothing second)
        {
            return false;
        }
    }
}
