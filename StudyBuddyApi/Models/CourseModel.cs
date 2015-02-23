using System;
using System.Collections.Generic;

namespace SpeedyDonkeyApi.Models
{
    public class CourseModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GradeType { get; set; }

        public IList<ProfessorModel> Professors { get; set; }
        public IList<NoticeModel> Notices { get; set; }
        public IList<LectureModel> Lectures { get; set; } 
        public IList<AssignmentModel> Assignments { get; set; } 
        public IList<ExamModel> Exams { get; set; }
        public IList<StudentModel> Students { get; set; } 

        public CourseModel()
        {
            Professors = new List<ProfessorModel>();
            Notices = new List<NoticeModel>();
            Lectures = new List<LectureModel>();
            Assignments = new List<AssignmentModel>();
            Exams = new List<ExamModel>();
            Students = new List<StudentModel>();
        }
    }
}