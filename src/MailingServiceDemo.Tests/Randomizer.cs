using System;

namespace MailingServiceDemo.Tests
{
    public static class Randomizer
    {
        private static readonly Random _random = new Random();

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }
}