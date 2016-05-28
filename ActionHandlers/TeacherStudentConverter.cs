using System;
using ActionHandlers.Teachers.TeacherRates;
using Common.Extensions;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public interface ITeacherStudentConverter
    {
        Teacher AddAsTeacher(int studentId);
        User RemoveAsTeacher(int teacherId);
    }

    public class TeacherStudentConverter : ITeacherStudentConverter
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IRepository<User> _studentRepository;
        private readonly ITeacherRateFactory _teacherRateFactory;

        public TeacherStudentConverter(
            IRepository<Teacher> teacherRepository, 
            IRepository<User> studentRepository,
            ITeacherRateFactory teacherRateFactory)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _teacherRateFactory = teacherRateFactory;
        }

        public Teacher AddAsTeacher(int studentId)
        {
            var student = _studentRepository.Get(studentId);

            student.Claims = new AppendClaimToUser(student.Claims, Claim.Teacher)
                .Do();

            var teacher = new Teacher(student);
            teacher.Rate = _teacherRateFactory.CreateDefault();

            return _teacherRepository.Create(teacher);
        }

        public User RemoveAsTeacher(int teacherId)
        {
            var teacher = _teacherRepository.Get(teacherId);
            var student = (User) teacher.User;
            
            student.Claims = new RemoveClaimFromUser(student.Claims, Claim.Teacher).Do();

            _teacherRepository.Delete(teacherId);
            return student;
        }
    }

    public class AppendClaimToUser : ICodeChunk<string>
    {
        private readonly string _currentClaims;
        private readonly Claim _claim;

        public AppendClaimToUser(string currentClaims, Claim claim)
        {
            _currentClaims = currentClaims;
            _claim = claim;
        }

        public string Do()
        {
            if (String.IsNullOrWhiteSpace(_currentClaims))
                return _claim.ToString();

            if (!_currentClaims.Contains(_claim.ToString()))
                return _currentClaims + "," + _claim;

            return _currentClaims;
        }
    }

    public class RemoveClaimFromUser : ICodeChunk<string>
    {
        private readonly string _currentClaims;
        private readonly Claim _claim;

        public RemoveClaimFromUser(string currentClaims, Claim claim)
        {
            _currentClaims = currentClaims;
            _claim = claim;
        }

        public string Do()
        {
            return _currentClaims.IsNotNullOrWhiteSpace() 
                ? _currentClaims.Replace(_claim.ToString(), "") 
                : _currentClaims;
        }
    }
}