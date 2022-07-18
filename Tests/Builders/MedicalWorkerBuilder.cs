using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Tests.Builders
{
    public class MedicalWorkerBuilder
    {
        private Address address = new Address("Łódź","96-222","Kolorowa",22,12);
        private DateTime dateOfBirth = new DateTime(1990, 04, 12);
        private static IGenericCounter<User> _userCounter = new Mock<IGenericCounter<User>>().Object;
        private User user = new UserBuilder().InjectInterface(_userCounter).Build();
        private List<MedicalWorkerProfession> medicalWorkerProfessions = new List<MedicalWorkerProfession>();
        private static IGenericCounter<MedicalWorker> _medicalWorkerCounter = new Mock<IGenericCounter<MedicalWorker>>().Object;

        public static MedicalWorkerBuilder Create()
        {
            return new MedicalWorkerBuilder();
        }
        public MedicalWorkerBuilder Address(string city,string zipCode, string street,int houseNumber, int? apartmentNumber)
        {
            this.address = new Address(city, zipCode, street, houseNumber, apartmentNumber);
            return this;
        }
        public MedicalWorkerBuilder DateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
            return this;
        }
        public MedicalWorkerBuilder AddUser(Action<UserBuilder> userBuilderAction)
        {
            var userBuilder = new UserBuilder();
            userBuilderAction(userBuilder);
            user = userBuilder.InjectInterface(_userCounter).Build();
            return this;
        }
       
        public MedicalWorkerBuilder AddMedicalWorkerProfession(MedicalWorkerProfession medicalWorkerRole)
        {
            medicalWorkerProfessions.Add(medicalWorkerRole);
            return this;
        }
        public MedicalWorkerBuilder InjectInterface(IGenericCounter<MedicalWorker> medicalWorkerCounter)
        {
            _medicalWorkerCounter = medicalWorkerCounter;
            return this;
        }
        public MedicalWorker Build()
        {
            return MedicalWorker.Create(address, dateOfBirth, user, _medicalWorkerCounter);

        }
    }
}
