using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Pupil
{
    public class PupilViewModel : View
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ClassName { get; set; }
    }
}
