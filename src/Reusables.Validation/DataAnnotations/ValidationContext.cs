using System;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Validation.DataAnnotations
{
    public class ValidationContext
    {
        public ValidationContext(Type objectType, string memberName)
        {
            Requires.IsNotNull(objectType, nameof(objectType));
            Requires.IsNotNull(memberName, nameof(memberName));

            ObjectType = objectType;
            MemberName = memberName;
        }

        public Type ObjectType { get; private set; }

        public string MemberName { get; private set; }
    }
}
