using System;

namespace Reusables.Validation.DataAnnotations
{
    public class ValidationContext
    {
        public ValidationContext(Type objectType, string memberName)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            if (memberName == null)
            {
                throw new ArgumentNullException("memberName");
            }

            ObjectType = objectType;
            MemberName = memberName;
        }

        public Type ObjectType { get; private set; }

        public string MemberName { get; private set; }
    }
}
