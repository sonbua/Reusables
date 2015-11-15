using System;

namespace CqrsEventSourcingDemo.Web.Abstractions.Views
{
    public abstract class ViewModel
    {
        public Guid Id { get; set; }
    }
}