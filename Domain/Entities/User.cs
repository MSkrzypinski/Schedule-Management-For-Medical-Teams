using Domain.DDDBlocks;

using Domain.Entities.Rules.UserRules;
using Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : Entity
    {
        public Guid Id { get; }
        public Name Name { get; }
        public Password Password { get; }
        public PhoneNumber PhoneNumber { get; }
        public Email Email { get; }
        public List<UserRole> UserRoles { get; }
        public bool IsActive {get;set;}

        private User()
        {
            //For EF
        }

        private User(Name name, Password password, PhoneNumber phoneNumber, Email email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = email;
            UserRoles = new List<UserRole>();
            IsActive = true;
        }
        private User(Name name, Password password, PhoneNumber phoneNumber, Email email, IEnumerable<UserRole> userRoles) : this(name,password,phoneNumber,email)
        {
            UserRoles = new List<UserRole>(userRoles);
        }

        public static User RegisterNewUser(
            Name name, 
            Password password, 
            PhoneNumber phoneNumber, 
            Email email, 
            IGenericCounter<User> userCounter, 
            IEnumerable<UserRole> userRoles = null)
        {
            if (name == null)
                throw new ArgumentException("Name cannot be null");
            if (password == null)
                throw new ArgumentException("Password cannot be null");
            if (phoneNumber == null)
                throw new ArgumentException("Phone number cannot be null");
            if (email == null)
                throw new ArgumentException("Email cannot be null");

            UserMustBeUnique.Check(email, userCounter);

            if (userRoles != null)
                return new User(name, password, phoneNumber, email, userRoles);
            else
                return new User(name, password, phoneNumber, email);
        }
       
    }
}
