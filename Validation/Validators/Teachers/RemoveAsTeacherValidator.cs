using Actions;
using Data.Repositories;
using Models;

namespace Validation.Validators.Teachers
{
    public class RemoveAsTeacherValidator : PreExistingValidator<Teacher>, IActionValidator<RemoveAsTeacher, Teacher>
    {
        public RemoveAsTeacherValidator(IRepository<Teacher> repository)
            : base(repository)
        {
        }
    }
}