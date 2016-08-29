using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.EventSourcing.Extensions
{
    public static class AggregateExtensions
    {
        private static readonly MethodInfo _internalPreserveStackTraceMethod = typeof (Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void ApplyEventOptionally<TAggregate, TEvent>(this TAggregate aggregate, TEvent @event) where TAggregate : Aggregate
        {
            MethodInfo handler;
            var eventType = @event.GetType();

            if (!EventHandlerCache<TAggregate>.Instance.TryGetValue(eventType, out handler))
                return;

            try
            {
                handler.Invoke(aggregate, new object[] {@event});
            }
            catch (TargetInvocationException ex)
            {
                _internalPreserveStackTraceMethod?.Invoke(ex.InnerException, new object[0]);

                throw ex.InnerException;
            }
        }

        private static class EventHandlerCache<TAggregate>
        {
            public static readonly IDictionary<Type, MethodInfo> Instance = typeof (TAggregate).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                                                                               .Where(m => m.Name == "When")
                                                                                               .Where(m => m.GetParameters().Length == 1)
                                                                                               .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }
    }
}
