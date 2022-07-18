using Domain.Entities;
using Domain.Entities.Rules.UserRules;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace Tests.Builders
{
    public class UserBuilder
    {
        private Name name = new Name("John", "Sock");
        private Password password = new Password("JohnSock");
        private PhoneNumber phoneNumber = new PhoneNumber("594839483");
        private Email email = new Email("JohnSock@john.com");
        private List<UserRole> userRoles = new List<UserRole>();
        private static IGenericCounter<User> _userCounter = new Mock<IGenericCounter<User>>().Object;

        public static UserBuilder Create()
        {
            return new UserBuilder();
        }
       
        public UserBuilder Name(string firstName, string lastName)
        {
            this.name = new Name(firstName, lastName);
            return this;
        }
        public UserBuilder Password(string value)
        {
            this.password = new Password(value);
            return this;
        }
        public UserBuilder PhoneNumber(string value)
        {
            this.phoneNumber = new PhoneNumber(value);
            return this;
        }
        public UserBuilder Email(string value)
        {
            this.email = new Email(value);
            return this;
        }
        public UserBuilder InjectInterface(IGenericCounter<User> userCounter) 
        {
            _userCounter = userCounter;
            return this;
        }
        public User Build()
        {
            return User.RegisterNewUser(name, password, phoneNumber, email, _userCounter, userRoles);
        }

    }
}
