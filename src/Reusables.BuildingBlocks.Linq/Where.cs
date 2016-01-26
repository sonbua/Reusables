using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks.Linq
{
    public class Where<TSource> : IRequestHandler<IEnumerable<TSource>, IEnumerable<TSource>>
    {
        private readonly Func<TSource, bool> _predicate;

        private Where(Func<TSource, bool> predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<TSource> Handle(IEnumerable<TSource> request)
        {
            return request.Where(_predicate);
        }

        public static IRequestHandler<IEnumerable<TSource>, IEnumerable<TSource>> Apply(Func<TSource, bool> predicate)
        {
            Requires.IsNotNull(predicate, nameof(predicate));

            return new Where<TSource>(predicate);
        }
    }
}
