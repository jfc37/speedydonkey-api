using System;
using System.Collections.Generic;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public interface ITeacherStudentConverter
    {
        Teacher ToTeacher(int studentId);
        User ToStudent(int teacherId);
    }

    public class TeacherStudentConverter : ITeacherStudentConverter
    {
        private readonly ICommonInterfaceCloner _cloner;
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IRepository<User> _studentRepository;

        public TeacherStudentConverter(ICommonInterfaceCloner cloner, IRepository<Teacher> teacherRepository, IRepository<User> studentRepository)
        {
            _cloner = cloner;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }

        public Teacher ToTeacher(int studentId)
        {
            var student = _studentRepository.Get(studentId);
            var teacher = _cloner.Clone<User, Teacher>(student);

            CopyProperties(student, teacher);

            AddTeacherClaim(teacher);
            var createdTeacher = _teacherRepository.Create(teacher);

            RemoveAssociations(student);
            return createdTeacher;
        }

        public User ToStudent(int teacherId)
        {
            var teacher = _teacherRepository.Get(teacherId);
            var student = _cloner.Clone<Teacher, User>(teacher);

            CopyProperties(teacher, student);
            RemoveTeacherClaim(student);
            var createdStudent = _studentRepository.Create(student);

            RemoveAssociations(teacher);
            return createdStudent;
        }

        private void RemoveAssociations(User user)
        {
            user.Passes.Clear();
            user.EnroledBlocks.Clear();
            user.Schedule.Clear();
            _studentRepository.Delete(user.Id);
        }

        private void CopyProperties(User fromUser, User toUser)
        {
            toUser.EnroledBlocks = new List<IBlock>(fromUser.EnroledBlocks);
            toUser.Passes = new List<IPass>(fromUser.Passes);
            toUser.Schedule = new List<IBooking>(fromUser.Schedule);
            toUser.Claims = fromUser.Claims;
            toUser.ActivationKey = fromUser.ActivationKey;
            toUser.Status = fromUser.Status;
        }

        private void AddTeacherClaim(User userToMakeTeacher)
        {
            if (String.IsNullOrWhiteSpace(userToMakeTeacher.Claims))
                userToMakeTeacher.Claims = Claim.Teacher.ToString();
            else if (!userToMakeTeacher.Claims.Contains(Claim.Teacher.ToString()))
                userToMakeTeacher.Claims = userToMakeTeacher.Claims + "," + Claim.Teacher;
        }

        private void RemoveTeacherClaim(User userToMakeTeacher)
        {
            if (!String.IsNullOrWhiteSpace(userToMakeTeacher.Claims))
                userToMakeTeacher.Claims = userToMakeTeacher.Claims.Replace(Claim.Teacher.ToString(), "");
        }
    }
}