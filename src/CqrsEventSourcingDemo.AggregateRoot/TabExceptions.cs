using System;

namespace CqrsEventSourcingDemo.AggregateRoot
{
    public class TabNotOpenException : Exception
    {
    }

    public class TabHasUnservedItemsException : Exception
    {
    }

    public class MustPayEnoughException : Exception
    {
    }
}