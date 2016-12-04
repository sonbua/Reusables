using System;

namespace CqrsEventSourcingDemo.ReadModel
{
    public abstract class ReadModel
    {
        public Guid Id { get; set; }
    }
}