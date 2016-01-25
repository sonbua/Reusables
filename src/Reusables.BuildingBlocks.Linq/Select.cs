using System;
using System.Collections.Generic;
using System.Linq;

namespace Reusables.BuildingBlocks.Linq
{
    public class Select<TSource, TResult> : IRequestHandler<IEnumerable<TSource>, IEnumerable<TResult>>
    {
        private readonly Func<TSource, TResult> _selector;

        public Select(Func<TSource, TResult> selector)
        {
            _selector = selector;
        }

        public IEnumerable<TResult> Handle(IEnumerable<TSource> request)
        {
            return request.Select(_selector);
        }
    }
}
