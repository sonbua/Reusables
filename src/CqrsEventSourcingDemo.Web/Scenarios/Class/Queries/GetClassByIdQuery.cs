using System;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Queries
{
    public class GetClassByIdQuery : Query<ClassViewModel>
    {
        public Guid Id { get; set; }
    }
}
