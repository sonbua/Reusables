using System;
using System.Collections.Generic;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks.Linq
{
    public class ForEach<TSource> : IRequestHandler<IEnumerable<TSource>, Nothing>
    {
        private readonly Action<TSource> _action;

        private ForEach(Action<TSource> action)
        {
            _action = action;
        }

        public Nothing Handle(IEnumerable<TSource> request)
        {
            foreach (var item in request)
                _action(item);

            return new Nothing();
        }

        public static IRequestHandler<IEnumerable<TSource>, Nothing> Apply(Action<TSource> action)
        {
            Requires.IsNotNull(action, nameof(action));

            return new ForEach<TSource>(action);
        }
    }
}
