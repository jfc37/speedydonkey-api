using System;
using System.Net.Http;
using Common.Tests.Builders.MockBuilders;
using Models;
using Moq;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders.MockBuilders
{
    public class MockModelFactoryBuilder : MockBuilder<IModelFactory>
    {
        public MockModelFactoryBuilder WithEntityToModelMapping()
        {
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Course>()))
                .Returns<HttpRequestMessage, Course>((x, y) => new CourseModel { Id = y.Id});
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Person>()))
                .Returns<HttpRequestMessage, Person>((x, y) => new StudentModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Student>()))
                .Returns<HttpRequestMessage, Student>((x, y) => new StudentModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Professor>()))
                .Returns<HttpRequestMessage, Professor>((x, y) => new ProfessorModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<User>()))
                .Returns<HttpRequestMessage, User>((x, y) => new UserModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Notice>()))
                .Returns<HttpRequestMessage, Notice>((x, y) => new NoticeModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Lecture>()))
                .Returns<HttpRequestMessage, Lecture>((x, y) => new LectureModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Assignment>()))
                .Returns<HttpRequestMessage, Assignment>((x, y) => new AssignmentModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Exam>()))
                .Returns<HttpRequestMessage, Exam>((x, y) => new ExamModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseGrade>()))
                .Returns<HttpRequestMessage, CourseGrade>((x, y) => new CourseGradeModel { Id = y.Id });
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseWorkGrade>()))
                .Returns<HttpRequestMessage, CourseWorkGrade>((x, y) => new CourseWorkGradeModel { Id = y.Id });
            return this;
        }

        public MockModelFactoryBuilder WithModelMappingReturningNull()
        {
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Course>()))
                .Returns((CourseModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Person>()))
                .Returns((StudentModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Student>()))
                .Returns((StudentModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Professor>()))
                .Returns((ProfessorModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<User>()))
                .Returns((UserModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Notice>()))
                .Returns((NoticeModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Lecture>()))
                .Returns((LectureModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Assignment>()))
                .Returns((AssignmentModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Exam>()))
                .Returns((ExamModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseGrade>()))
                .Returns((CourseGradeModel)null);
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseWorkGrade>()))
                .Returns((CourseWorkGradeModel)null);
            return this;
        }

        public MockModelFactoryBuilder WithModelParsing()
        {
            Mock.Setup(x => x.Parse(It.IsAny<UserModel>()))
                .Returns<UserModel>(x => new User {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<PersonModel>()))
                .Returns<PersonModel>(x => new Student {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<StudentModel>()))
                .Returns<StudentModel>(x => new Student {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<ProfessorModel>()))
                .Returns<ProfessorModel>(x => new Professor {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<CourseModel>()))
                .Returns<CourseModel>(x => new Course {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<NoticeModel>()))
                .Returns<NoticeModel>(x => new Notice {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<LectureModel>()))
                .Returns<LectureModel>(x => new Lecture {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<AssignmentModel>()))
                .Returns<AssignmentModel>(x => new Assignment {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<ExamModel>()))
                .Returns<ExamModel>(x => new Exam {Id = x.Id});
            Mock.Setup(x => x.Parse(It.IsAny<CourseWorkGradeModel>()))
                .Returns<CourseWorkGradeModel>(x => new CourseWorkGrade { Id = x.Id });
            return this;
        }

        public MockModelFactoryBuilder WithModelParsingBlowUp()
        {
            Mock.Setup(x => x.Parse(It.IsAny<UserModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<PersonModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<StudentModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<ProfessorModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<CourseModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<NoticeModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<LectureModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<AssignmentModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<ExamModel>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.Parse(It.IsAny<CourseWorkGradeModel>()))
                .Throws<ArgumentException>();
            return this;
        }

        public void WithModelMappingBlowUp()
        {
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<User>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Person>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Student>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Professor>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Course>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Notice>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Lecture>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Assignment>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<Exam>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseGrade>()))
                .Throws<ArgumentException>();
            Mock.Setup(x => x.ToModel(It.IsAny<HttpRequestMessage>(), It.IsAny<CourseWorkGrade>()))
                .Throws<ArgumentException>();
        }
    }
}