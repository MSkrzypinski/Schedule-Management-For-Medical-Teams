using Domain.DDDBlocks;

using Domain.Entities.Rules.ScheduleRules;
using Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Schedule : Entity
    {
        public Guid Id { get; set; }
        public MedicalTeam MedicalTeam { get; }
        public bool IsPublished { get; set; }
        public MonthAndYearOfSchedule MonthAndYearOfSchedule { get; }
        public DateTime Created { get; }
        public List<Shift> Shifts { get; }
        private Schedule()
        {
            //For EF
        }
        private Schedule(MedicalTeam medicalTeam, MonthAndYearOfSchedule monthAndYearOfSchedule)
        {
            Id = Guid.NewGuid();
            MedicalTeam = medicalTeam;
            IsPublished = false;
            Created = DateTime.UtcNow;
            MonthAndYearOfSchedule = monthAndYearOfSchedule;
            Shifts = new List<Shift>(GenerateShifts());
        }
        public static Schedule Create(MedicalTeam medicalTeam,MonthAndYearOfSchedule monthAndYearOfSchedule, IGenericCounter<Schedule> scheduleCounter)
        {
            if (medicalTeam == null)
                throw new ArgumentException("Medical team cannot be null");
            if (monthAndYearOfSchedule == null)
                throw new ArgumentException("Month and year cannot be null");

            ScheduleMustBeOnlyOneForSpecificDateAndTeam.Check(medicalTeam,monthAndYearOfSchedule ,scheduleCounter);
            return new Schedule(medicalTeam,monthAndYearOfSchedule);
        }   
        public void Publish()
        {
            AllShiftsMustBePublished.Check(this);
            IsPublished = true;
        }
        private List<Shift> GenerateShifts()
        {
            List<Shift> shifts = new List<Shift>();
            
            for (int i = 1; i <= DateTime.DaysInMonth(MonthAndYearOfSchedule.Year, MonthAndYearOfSchedule.Month); i++)
            {
                shifts.Add(Shift.Create(new DateRange(new DateTime(MonthAndYearOfSchedule.Year, MonthAndYearOfSchedule.Month, i, 7, 0, 0),
                                                    new DateTime(MonthAndYearOfSchedule.Year, MonthAndYearOfSchedule.Month, i, 19, 0, 0)), MedicalTeam, this));

                shifts.Add(Shift.Create(new DateRange(new DateTime(MonthAndYearOfSchedule.Year, MonthAndYearOfSchedule.Month, i, 19, 0, 0),
                                                    new DateTime(MonthAndYearOfSchedule.Year, MonthAndYearOfSchedule.Month, i, 7, 0, 0).AddDays(1)), MedicalTeam, this));
            }

            return shifts;
        }
       
    }
}
