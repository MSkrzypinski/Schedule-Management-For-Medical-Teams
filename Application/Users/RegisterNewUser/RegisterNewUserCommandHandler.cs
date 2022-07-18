using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, RegisterNewUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IGenericCounter<Domain.Entities.User> _genericCounter;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

        public RegisterNewUserCommandHandler
            (IUserRepository userRepository, 
            IMapper mapper,
            IGenericCounter<Domain.Entities.User> genericCounter,
            IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _genericCounter = genericCounter;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterNewUserCommandResponse> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterNewUserValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new RegisterNewUserCommandResponse(validatorResult);
            }
            var mapUser = _mapper.Map<Domain.Entities.User>(request);
            
            var hashedPassword = _passwordHasher.HashPassword(mapUser,request.Password.Value);

            var password = _mapper.Map<string, Password>(hashedPassword);

            var user = Domain.Entities.User.RegisterNewUser(
                mapUser.Name,
                password,
                mapUser.PhoneNumber,
                mapUser.Email,
                _genericCounter
                );

            await _userRepository.AddAsync(user);

            return new RegisterNewUserCommandResponse(user.Id);
        }
    }
}
