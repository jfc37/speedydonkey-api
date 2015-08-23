using Common.Extensions;
using Data.CodeChunks;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Services
{
    /// <summary>
    /// Maps a Teacher to Teacher Model, taking the user's name
    /// </summary>
    public class TeacherToTeacherModelMapping : ICodeChunk<TeacherModel>
    {
        private readonly ITeacher _teacher;

        public TeacherToTeacherModelMapping(ITeacher teacher)
        {
            _teacher = teacher;
        }

        public TeacherModel Do()
        {
            var model = new TeacherModel {Id = _teacher.Id};
            if (model.User.IsNotNull())
            {
                model.FirstName = model.User.FirstName;
                model.Surname = model.User.Surname;
            }

            return model;
        }
    }
}