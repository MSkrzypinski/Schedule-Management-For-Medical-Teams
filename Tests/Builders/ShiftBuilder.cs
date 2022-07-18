using Domain.Entities;

using Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Builders
{
    public class ShiftBuilder
    {
        private DateRange dateRange = new DateRange(new DateTime(DateTime.Now.Year,2,10,7,0,0), new DateTime(DateTime.Now.Year, 2, 10, 19, 0, 0));
        private MedicalTeam medicalTeam = new MedicalTeamBuilder().Build();
        private Schedule schedule = new ScheduleBuilder().Build();

        public static ShiftBuilder Create()
        {
            return new ShiftBuilder();
        }
        public ShiftBuilder AddDateRange(DateTime start, DateTime end)
        {
            dateRange = new DateRange(start, end);
            return this;
        }
        public ShiftBuilder AddMedicalTeam(Action<MedicalTeamBuilder> action)
        {
            var medicalTeamBuilder = new MedicalTeamBuilder();
            action(medicalTeamBuilder);
            medicalTeam = medicalTeamBuilder.Build();
            return this;
        }
   
        public Shift Build()
        {
           return Shift.Create(dateRange, medicalTeam, schedule);
        }

    }
}
