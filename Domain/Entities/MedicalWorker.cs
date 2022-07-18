using Domain.DDDBlocks;

using Domain.Entities.Rules.MedicalWorkerRules;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Domain.Entities
{
    public class MedicalWorker : Entity
    {
        public Guid Id { get; set; }
        public Address Address { get; }
        public DateTime DateOfBirth { get; }
        public List<EmploymentContract> EmploymentContracts { get; }
        public User User { get; }
        public List<Shift> Shifts { get; }
        public List<DayOff> DaysOff { get; }
        public List<MedicalWorkerProfession> MedicalWorkerProfessions { get; }
        public MedicalWorker()
        {
            //For EF
        }
        private MedicalWorker(Address address, DateTime dateOfBirth, User user)
        {
            if (address == null)
                throw new ArgumentException("Address cannot be null");
            if (dateOfBirth == null)
                throw new ArgumentException("Date of birth cannot be null");
            if (user == null)
                throw new ArgumentException("User cannot be null");

            Id = Guid.NewGuid();
            Address = address; 
            DateOfBirth = dateOfBirth;
            User = user;
            EmploymentContracts = new List<EmploymentContract>();
            Shifts = new List<Shift>();
            DaysOff = new List<DayOff>();
            MedicalWorkerProfessions = new List<MedicalWorkerProfession>();

            User.UserRoles.Add(UserRole.MedicalWorker());
        }

        public static MedicalWorker Create(Address address, DateTime dateOfBirth, User user, IGenericCounter<MedicalWorker> medicalWorkerCounter)
        {
            if (address == null)
                throw new ArgumentException("Address cannot be null");
            
            if (user == null)
                throw new ArgumentException("User cannot be null");

            UserCannotBeAssignedToMedicalWorkerMoreThanOnce.Check(user, medicalWorkerCounter);

            return new MedicalWorker(address, dateOfBirth, user);
        }

        public void AddMedicalWorkerProfession(MedicalWorkerProfessionEnum medicalWorkerProfession)
        {
            MedicalWorkerProfessions.Add(MedicalWorkerProfession.Create(medicalWorkerProfession));
        }
        public void AddDayOff(DateTime start,DateTime end)
        {
            DaysOffCannotBeAddedInTheLast10DaysOfMonth.Check();
            DaysOffCanOnlyBeAddedForTheNextMonth.Check(start,end);

            DaysOff.Add(DayOff.Create(start, end));

            UnionTheOverlapDaysOff();
        }
        public void AddEmploymentContract(MedicalTeam medicalTeam,ContractType contractType,MedicRole medicRole,MedicalWorkerProfessionEnum medicalWorkerProfession)
        {
            EmploymentContracts.Add(EmploymentContract.Create(this,medicalTeam, contractType,medicRole,medicalWorkerProfession));
        }
        private void UnionTheOverlapDaysOff()
        {
            var daysOffArray = this.DaysOff.OrderBy(x => x.Start).ToArray();

            this.DaysOff.Clear();

            for (int i = 0; i < daysOffArray.Length - 1; i++)
            {
                if (daysOffArray[i].Includes(daysOffArray[i + 1].Start, daysOffArray[i + 1].End))
                {
                    var startDayOff = daysOffArray[i].Start;
                    var endDayOff = daysOffArray[i].End > daysOffArray[i + 1].End ? daysOffArray[i].End : daysOffArray[i + 1].End;
                    daysOffArray[i + 1] = new DayOff(startDayOff, endDayOff);
                }
                else
                {
                    DaysOff.Add(daysOffArray[i]);
                }
            }
            DaysOff.Add(daysOffArray[daysOffArray.Length - 1]);
        }
    }
}
