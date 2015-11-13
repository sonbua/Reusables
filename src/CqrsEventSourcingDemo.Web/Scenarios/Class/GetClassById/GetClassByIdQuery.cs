using System;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.GetClassById
{
    public class GetClassByIdQuery : Query<ClassView>
    {
        public Guid Id { get; set; }
    }
}
