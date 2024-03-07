using Application.Mapper.Dtos;
using Application.Persistence;
using AutoMapper;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using Application.User.Authentication;
using FluentValidation;

namespace Application.Users.Authentication
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;
        private readonly JwtConfig _jwtConfig;
        public AuthenticationCommandHandler
            (IUserRepository userRepository,
            IMedicalWorkerRepository medicalWorkerRepository,
            IMapper mapper, 
            IPasswordHasher<Domain.Entities.User> passwordHasher, 
            IOptions<JwtConfig> jwtConfig)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtConfig = jwtConfig.Value;
            _medicalWorkerRepository = medicalWorkerRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            var validator = new AuthenticationCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException("Validaton failed");
            }

            var email = _mapper.Map<string, Email>(request.Email);

            var user = await _userRepository.FindByEmailAsync(email);
            
            if (user == null)
            {
                throw new ArgumentNullException("Incorrect login or password");
            }
  
            var result = _passwordHasher.VerifyHashedPassword(user,user.Password.Value,request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Incorrect login or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Name.FirstName} {user.Name.LastName}"),
                new Claim("UserID",user.Id.ToString())
            };
            user.UserRoles.ForEach(x => claims.Add( new Claim(ClaimTypes.Role, x.Value)));
 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: credentials);

            return new AuthenticationResponse(user.Email.Value, new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
