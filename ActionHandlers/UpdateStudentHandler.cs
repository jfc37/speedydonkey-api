using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateStudentHandler : IActionHandler<UpdateStudent, Student>
    {
        private readonly IPersonRepository<Student> _studentRepository;

        public UpdateStudentHandler(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student Handle(UpdateStudent action)
        {
            var originalStudent = _studentRepository.Get(action.ActionAgainst.Id);
            originalStudent.FirstName = action.ActionAgainst.FirstName;
            originalStudent.Surname = action.ActionAgainst.Surname;

            return _studentRepository.Update(action.ActionAgainst);
        }
    }
}
