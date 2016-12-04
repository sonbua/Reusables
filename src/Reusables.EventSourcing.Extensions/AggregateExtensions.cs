using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.EventSourcing.Extensions
{
    public static class AggregateExtensions
    {
        private static readonly MethodInfo _internalPreserveStackTraceMethod = typeof (Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Does reflection on <typeparamref name="TAggregate" /> to find correct "When" method that can handle event of type <typeparamref name="TEvent" />. If no method is found, then just ignore this event.
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="aggregate"></param>
        /// <param name="event"></param>
        public static void ApplyInternally<TAggregate, TEvent>(this TAggregate aggregate, TEvent @event) where TAggregate : Aggregate
        {
            MethodInfo handler;
            var eventType = @event.GetType();

            if (!InternalEventHandlerMap<TAggregate>.Instance.TryGetValue(eventType, out handler))
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

        /// <summary>
        /// Stores in memory a map of event to "When" method, which handles that event.
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        private static class InternalEventHandlerMap<TAggregate>
        {
            static InternalEventHandlerMap()
            {
                Instance =
                    typeof(TAggregate)
                        .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(m => m.Name == "When")
                        .Where(m => m.GetParameters().Length == 1)
                        .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
            }

            public static readonly IDictionary<Type, MethodInfo> Instance;
        }
    }
}
