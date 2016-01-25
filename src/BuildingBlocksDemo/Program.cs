using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.BuildingBlocks;
using Reusables.BuildingBlocks.Linq;

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

            var handler = new RangeEnumerator().FollowedBy(new Select<int, int>(x => x*x*x))
                                               .FollowedBy(new Select<int, int>(x => x/2))
                                               .FollowedBy(new Where<int>(x => x%2 == 1))
                                               .FollowedBy(new ForEach<int>(Console.WriteLine));

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

    internal class RangeRequest
    {
        public int Start { get; set; }

        public int Count { get; set; }
    }
}
