using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks.Linq
{
    public class Select<TSource, TResult> : IRequestHandler<IEnumerable<TSource>, IEnumerable<TResult>>
    {
        private readonly Func<TSource, TResult> _selector;

        private Select(Func<TSource, TResult> selector)
        {
            _selector = selector;
        }

        public IEnumerable<TResult> Handle(IEnumerable<TSource> request)
        {
            return request.Select(_selector);
        }

        public static IRequestHandler<IEnumerable<TSource>, IEnumerable<TResult>> With(Func<TSource, TResult> selector)
        {
            Requires.IsNotNull(selector, nameof(selector));

            return new Select<TSource, TResult>(selector);
        }
    }
}
