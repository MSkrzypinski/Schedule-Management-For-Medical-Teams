using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using MediatR;

namespace Application.MedicalWorkers.UpdateMedicalWorker
{
    public class UpdateMedicalWorkerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string City { get; set;}
        public string ZipCode { get; set;}
        public string Street { get; set;}
        public int HouseNumber { get; set;}
        public int? ApartmentNumber { get; set;}
        public DateTime DateOfBirth { get; set; }
    }
}