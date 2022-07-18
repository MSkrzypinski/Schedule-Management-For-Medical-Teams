using Domain.DDDBlocks;

using Domain.Entities.Rules.CoordinatorRules;
using Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Coordinator : Entity
    {
        public Guid Id { get; }
        public User User { get; }
        public List<MedicalTeam> MedicalTeams { get; }
        private Coordinator()
        {
            //For EF
        }
        private Coordinator(User user)
        {
            Id = Guid.NewGuid();
            User = user;
            MedicalTeams = new List<MedicalTeam>();

            User.UserRoles.Add(UserRole.Coordinator()); 
        }
        private Coordinator(User user, IEnumerable<MedicalTeam> medicalTeams) : this(user)
        {
            MedicalTeams = new List<MedicalTeam>(medicalTeams);
        }
        public static Coordinator Create(User user,IGenericCounter<Coordinator> coordinatorCounter)
        {
            if (user == null)
                throw new ArgumentException("User cannot be null");

            UserCannotBeAssignedToCoordinatorMoreThanOnce.Check(user, coordinatorCounter);

            return new Coordinator(user);
        }
        
    }
}
