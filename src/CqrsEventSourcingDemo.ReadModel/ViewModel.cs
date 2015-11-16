using System;

namespace CqrsEventSourcingDemo.ReadModel
{
    // TODO: remove this base class
    public abstract class ViewModel
    {
        public Guid Id { get; set; }
    }
}
