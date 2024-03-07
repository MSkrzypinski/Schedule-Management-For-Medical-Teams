using Domain.DDDBlocks;
using Domain.Entities.Rules.ShiftRules;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Shift : Entity
    {
        public Guid Id { get; set; }
        public DateRange DateRange { get; }
        public MedicalTeam MedicalTeam { get; }
        public Schedule Schedule { get; }
        public MedicalWorker Driver { get; private set; }
        public MedicalWorker Manager { get; private set; }
        public MedicalWorker CrewMember{ get; private set; }
        public readonly List<MedicalWorker> Crew;
        private Shift()
        {
            //For EF
        }
        private Shift(DateRange dateRange, MedicalTeam medicalTeam,Schedule schedule)
        {
            if (dateRange == null)
                throw new ArgumentException("Date range cannot be null");
            if (medicalTeam == null)
                throw new ArgumentException("Medical team cannot be null");
            if (schedule == null)
                throw new ArgumentException("Schedule cannot be null");

            Id = Guid.NewGuid();
            DateRange = dateRange;
            MedicalTeam = medicalTeam;
            Schedule = schedule;
            Driver = null;
            Manager = null;
            CrewMember = null;
            Crew = new List<MedicalWorker>();
        }
       
        public void AddOrChangeDriver(MedicalWorker medicalWorker)
        {
            //NumberOfMedicsMustBeLessThanOrEqualToMaximum.Check(MedicalTeam, this.GetQuantityOfMedics() + 1);
            MedicHaveContractForSpecificMedicTeam.Check(medicalWorker, MedicalTeam, MedicRole.Driver);
            DriverMustHaveAtLeastTwelveHoursBreakBeforeBeganTheNextShift.Check(medicalWorker, this);
            MedicCanWorkInThisTerm.Check(medicalWorker, this);
            if (Driver != null)
                Crew.Remove(Driver);
            Driver = medicalWorker;
            Crew.Add(Driver);
        }
        public void AddCrewMember(MedicalWorker medicalWorker)
        {
            MedicHaveContractForSpecificMedicTeam.Check(medicalWorker, MedicalTeam, MedicRole.RegularMedic);
            MedicCanWorkInThisTerm.Check(medicalWorker, this);
            //NumberOfMedicsMustBeLessThanOrEqualToMaximum.Check(MedicalTeam, this.GetQuantityOfMedics() + 1);
            if (CrewMember != null)
                Crew.Remove(CrewMember);
            CrewMember = medicalWorker;
            Crew.Add(CrewMember);
        }
       

        public void AddOrChangeCrewManager(MedicalWorker medicalWorker)
        {
            //NumberOfMedicsMustBeLessThanOrEqualToMaximum.Check(MedicalTeam, this.GetQuantityOfMedics()+1);
            MedicHaveContractForSpecificMedicTeam.Check(medicalWorker, MedicalTeam, MedicRole.Manager);
            MedicCanWorkInThisTerm.Check(medicalWorker, this);
            if (Manager != null)
                Crew.Remove(Manager);
            Manager = medicalWorker;
            Crew.Add(Manager);
        }

        public int GetQuantityOfMedics()
        {
            int sum = 0;
            if (Manager != null)
                sum += 1;
            if (Driver != null)
                sum += 1;
            if (CrewMember != null)
                sum += 1;

            return sum;
        }
        public void Publish()
        {
            if (Driver == null)
                throw new ArgumentException("Driver cannot be null");
            if (Manager == null)
                throw new ArgumentException("Manager cannot be null");

            ToPublishShiftNumberOfMedicsMustBeEqualToSizeOfTeam.Check(this);
            CrewMember.Shifts.Add(this);
            Driver.Shifts.Add(this);
            Manager.Shifts.Add(this);
        }
        public static Shift Create(DateRange dateRange, MedicalTeam medicalTeam,Schedule schedule)
        {
            return new Shift(dateRange, medicalTeam, schedule);
        }
    }
}
