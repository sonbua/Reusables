using System;
using System.Collections.Generic;

namespace Reusables.BuildingBlocks.Linq
{
    public class ForEach<TSource> : IRequestHandler<IEnumerable<TSource>, Nothing>
    {
        private readonly Action<TSource> _action;

        public ForEach(Action<TSource> action)
        {
            _action = action;
        }

        public Nothing Handle(IEnumerable<TSource> request)
        {
            foreach (var item in request)
            {
                _action(item);
            }

            return new Nothing();
        }
    }
}
