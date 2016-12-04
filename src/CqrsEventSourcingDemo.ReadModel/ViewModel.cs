using System;

namespace CqrsEventSourcingDemo.ReadModel
{
    // TODO: rename to ReadModel
    public abstract class ViewModel
    {
        public Guid Id { get; set; }
    }
}
