using System;
using System.Collections.Generic;
using System.Linq;
using Reusables;
using Reusables.BuildingBlocks;

namespace BuildingBlocksDemo
{
    internal class Program
    {
        private static void Main()
        {
            TradionalRoutine();

            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            BlockStyleRoutine();

            Console.ReadLine();
        }

        private static void BlockStyleRoutine()
        {
            var request = new RangeRequest {Start = 1, Count = 10};

            var handler = new RangeEnumerator().FollowedBy(new LinqSelect<int, int>(x => x*x*x))
                                               .FollowedBy(new LinqSelect<int, int>(x => x/2))
                                               .FollowedBy(new LinqWhere<int>(x => x%2 == 1))
                                               .FollowedBy(new LinqForEach<int>(Console.WriteLine));

            handler.Handle(request);
        }

        private static void TradionalRoutine()
        {
            Enumerable.Range(1, 10)
                      .Select(x => x*x*x)
                      .Select(x => x/2)
                      .Where(x => x%2 == 1)
                      .ToList()
                      .ForEach(Console.WriteLine);
        }
    }

    internal class RangeEnumerator : IRequestHandler<RangeRequest, IEnumerable<int>>
    {
        IEnumerable<int> IRequestHandler<RangeRequest, IEnumerable<int>>.Handle(RangeRequest request)
        {
            return Enumerable.Range(request.Start, request.Count);
        }
    }

    internal class LinqSelect<TIn, TOut> : IRequestHandler<IEnumerable<TIn>, IEnumerable<TOut>>
    {
        private readonly Func<TIn, TOut> _selector;

        public LinqSelect(Func<TIn, TOut> selector)
        {
            _selector = selector;
        }

        public IEnumerable<TOut> Handle(IEnumerable<TIn> request)
        {
            return request.Select(_selector);
        }
    }

    internal class LinqWhere<T> : IRequestHandler<IEnumerable<T>, IEnumerable<T>>
    {
        private readonly Func<T, bool> _predicate;

        public LinqWhere(Func<T, bool> predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<T> Handle(IEnumerable<T> request)
        {
            return request.Where(_predicate);
        }
    }

    internal class LinqForEach<T> : IRequestHandler<IEnumerable<T>, Nothing>
    {
        private readonly Action<T> _action;

        public LinqForEach(Action<T> action)
        {
            _action = action;
        }

        public Nothing Handle(IEnumerable<T> request)
        {
            foreach (var item in request)
            {
                _action(item);
            }

            return new Nothing();
        }
    }

    internal class RangeRequest
    {
        public int Start { get; set; }

        public int Count { get; set; }
    }
}
