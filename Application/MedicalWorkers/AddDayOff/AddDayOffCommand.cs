using Application.Mapper.Dtos;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.AddDayOff
{
    public class AddDayOffCommand : IRequest<Unit>
    {
        public Guid MedicalWorkerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
