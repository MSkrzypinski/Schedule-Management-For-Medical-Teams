using Application.Mapper.Dtos;
using Application.Responses;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.AddDayOff
{
    public class AddDayOffCommand : IRequest<AddDayOffCommandResponse>
    {
        public Guid MedicalWorkerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
