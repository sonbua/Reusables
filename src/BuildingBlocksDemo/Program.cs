using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.BuildingBlocks;
using Reusables.BuildingBlocks.Extensions;
using Reusables.BuildingBlocks.Linq;
using Reusables.Util.Extensions;

namespace BuildingBlocksDemo
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Traditional Routine");
            TradionalRoutine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Function Composition Routine");
            FunctionCompositionRoutine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Function Composition Using LINQ-like Syntax Routine");
            FunctionCompositionUsingLinqLikeSyntaxRoutine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Block-Style Routine");
            BlockStyleRoutine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            Console.WriteLine("Block-Style Using LINQ-like Syntax Routine");
            BlockStyleUsingLinqLikeSyntaxRoutine();

            Console.ReadLine();
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

        private static void FunctionCompositionRoutine()
        {
            Func<int, int, IEnumerable<int>> range = Enumerable.Range;

            var handler = range.ForwardCompose(Select<int, int>.Apply(x => x*x*x))
                               .ForwardCompose(Select<int, int>.Apply(x => x/2))
                               .ForwardCompose(Where<int>.Apply(x => x%2 == 1))
                               .ForwardCompose(ForEach<int>.Apply(Console.WriteLine));

            handler(1, 10);
        }

        private static void FunctionCompositionUsingLinqLikeSyntaxRoutine()
        {
            Func<int, int, IEnumerable<int>> range = Enumerable.Range;

            var handler = range.Select(x => x*x*x)
                               .Select(x => x/2)
                               .Where(x => x%2 == 1)
                               .ForEach(Console.WriteLine);

            handler(1, 10);
        }

        private static void BlockStyleRoutine()
        {
            var request = new RangeRequest {Start = 1, Count = 10};

            var handler = new RangeEnumerator().Forward(Select<int, int>.Apply(x => x*x*x))
                                               .Forward(Select<int, int>.Apply(x => x/2))
                                               .Forward(Where<int>.Apply(x => x%2 == 1))
                                               .Forward(ForEach<int>.Apply(Console.WriteLine));

            handler.Handle(request);
        }

        private static void BlockStyleUsingLinqLikeSyntaxRoutine()
        {
            var request = new RangeRequest {Start = 1, Count = 10};

            var handler = new RangeEnumerator().Select(x => x*x*x)
                                               .Select(x => x/2)
                                               .Where(x => x%2 == 1)
                                               .ForEach(Console.WriteLine);

            handler.Handle(request);
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
