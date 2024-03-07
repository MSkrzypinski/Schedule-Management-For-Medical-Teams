using Application.User.Authentication;
using AutoMapper;
using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Text;
using Domain.ValueObjects;
using Application.Mapper.Dtos;
using Domain.ValueObjects.Enums;
using Application.Users.RegisterNewUser;
using Application.MedicalTeams.UpdateMedicalTeam;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Name, NameDto>().ReverseMap();
            CreateMap<MedicalTeamDto, MedicalTeam>().ReverseMap();
            CreateMap<MedicalWorkerDto, MedicalWorker>().ReverseMap();
            CreateMap<Password, PasswordDto>().ReverseMap();
            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<Domain.Entities.User, UserDto>().ForMember(x => x.Id, a => a.MapFrom(s => s.Id));
            CreateMap<UserDto, Domain.Entities.User>().ReverseMap();
            CreateMap<AuthenticationCommand, Email>().ForMember(x => x.Value, o => o.MapFrom(k => k.Email));
            CreateMap<string, Email>().ConstructUsing(x => new Email(x));
            CreateMap<RegisterNewUserCommand,Domain.Entities.User>();
            CreateMap<Coordinator,CoordinatorDto>().ReverseMap();
            CreateMap<AddressDto, Address>();
            CreateMap<MedicalWorkerProfessionDto, MedicalWorkerProfession>();
            CreateMap<InformationAboutTeamDto, InformationAboutTeam>().ReverseMap();
            CreateMap<DayOffDto, DayOff>();
        }
    }
}
