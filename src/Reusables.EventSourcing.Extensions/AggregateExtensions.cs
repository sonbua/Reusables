using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.EventSourcing.Extensions
{
    public static class AggregateExtensions
    {
        private static readonly MethodInfo _internalPreserveStackTraceMethod = typeof (Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void InvokeEventOptional<TAggregate>(this TAggregate aggregate, object @event) where TAggregate : Aggregate
        {
            MethodInfo whenMethod;
            var eventType = @event.GetType();

            if (!Cache<TAggregate>.Dict.TryGetValue(eventType, out whenMethod))
            {
                return;
            }

            try
            {
                whenMethod.Invoke(aggregate, new[] {@event});
            }
            catch (TargetInvocationException ex)
            {
                _internalPreserveStackTraceMethod?.Invoke(ex.InnerException, new object[0]);

                throw ex.InnerException;
            }
        }

        private static class Cache<TAggregate>
        {
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof (TAggregate).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                                                                           .Where(m => m.Name == "When")
                                                                                           .Where(m => m.GetParameters().Length == 1)
                                                                                           .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }
    }
}
