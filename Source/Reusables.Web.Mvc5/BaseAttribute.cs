using System;

namespace Reusables.Web.Mvc5
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class BaseAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
