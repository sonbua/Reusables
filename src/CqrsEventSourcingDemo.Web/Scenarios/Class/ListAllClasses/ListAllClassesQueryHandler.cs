using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.ListAllClasses
{
    public class ListAllClassesQueryHandler : QueryHandler<ListAllClassesQuery, ClassDto[]>
    {


        public override ClassDto[] Handle(ListAllClassesQuery query)
        {
            return new ClassDto[0];
        }
    }
}