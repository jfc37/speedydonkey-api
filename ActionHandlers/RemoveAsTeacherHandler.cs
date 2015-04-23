using System;
using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class RemoveAsTeacherHandler : IActionHandler<RemoveAsTeacher, User>
    {
        private readonly IRepository<User> _repository;

        public RemoveAsTeacherHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Handle(RemoveAsTeacher action)
        {
            var userToRemoveAsTeacher = _repository.Get(action.ActionAgainst.Id);
            userToRemoveAsTeacher.TeachingConcerns = null;
            RemoveTeacherClaim(userToRemoveAsTeacher);
            _repository.Update(userToRemoveAsTeacher);

            return userToRemoveAsTeacher;
        }

        private void RemoveTeacherClaim(User userToMakeTeacher)
        {
            if (!String.IsNullOrWhiteSpace(userToMakeTeacher.Claims))
                userToMakeTeacher.Claims = userToMakeTeacher.Claims.Replace(Claim.Teacher.ToString(), "");
        }
    }
}
