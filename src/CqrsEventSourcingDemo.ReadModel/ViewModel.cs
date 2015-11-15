using System;

namespace CqrsEventSourcingDemo.ReadModel
{
    public abstract class ViewModel
    {
        public Guid Id { get; set; }
    }
}