using System;
using System.Collections.Generic;
using System.Linq;

namespace Reusables.BuildingBlocks.Linq
{
    public class Where<TSource> : IRequestHandler<IEnumerable<TSource>, IEnumerable<TSource>>
    {
        private readonly Func<TSource, bool> _predicate;

        public Where(Func<TSource, bool> predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<TSource> Handle(IEnumerable<TSource> request)
        {
            return request.Where(_predicate);
        }
    }
}
