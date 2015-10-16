using System;

namespace CommandQuery.Integration.Web.Mvc5.ActionFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class BaseAttribute : Attribute
    {
        public int Order { get; set; }
    }
}