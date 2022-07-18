
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities.Rules.CoordinatorRules
{
    public class UserCannotBeAssignedToCoordinatorMoreThanOnce
    {
        public static string Message = "User is already assigned to other coordinator";
        private static IGenericCounter<Coordinator> _coordinatorCounter;
        public static void Check(User user, IGenericCounter<Coordinator> coordinatorCounter)
        {
            _coordinatorCounter = coordinatorCounter;

            if (_coordinatorCounter.Count(x => x.User.Email.Equals(user.Email)).Result > 0)
            {
                throw new ApplicationException(Message);
            }
        }

    }
}
