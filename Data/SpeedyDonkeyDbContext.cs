using System.Data.Entity;
using Models;

namespace Data
{
    public interface ISpeedyDonkeyDbContext
    {
        IDbSet<Account> Accounts { get; set; }

        IDbSet<User> Users { get; set; }
        IDbSet<Person> People { get; set; }
        IDbSet<Course> Courses { get; set; }
        IDbSet<Notice> Notices { get; set; }
        IDbSet<Lecture> Lectures { get; set; }
        IDbSet<CourseWork> CourseWork { get; set; }
        IDbSet<CourseGrade> CourseGrades { get; set; }
        IDbSet<CourseWorkGrade> CourseWorkGrades { get; set; }

        int SaveChanges();
        void SetEntityState(object entity, EntityState state);

        IDbSet<TEntity> GetSetOfType<TEntity>() where TEntity : class;
    }

    public class SpeedyDonkeyDbContext : DbContext, ISpeedyDonkeyDbContext
    {
        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Person> People { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Notice> Notices { get; set; }
        public IDbSet<Lecture> Lectures { get; set; }
        public IDbSet<CourseWork> CourseWork { get; set; }
        public IDbSet<CourseGrade> CourseGrades { get; set; }
        public IDbSet<CourseWorkGrade> CourseWorkGrades { get; set; }

        public void SetEntityState(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }

        public IDbSet<TEntity> GetSetOfType<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().ToTable("Assignment");
            modelBuilder.Entity<Exam>().ToTable("Exam");

            //Not mapping as expected? Are you including the property in the repo?!

            //0-1 -> 1
            modelBuilder.Entity<User>().HasOptional(p => p.Person).WithRequired(l => l.User);

            //0-* -> 1-*
            modelBuilder.Entity<Professor>().HasMany(p => p.Courses).WithMany(c => c.Professors);

            //* - 1
            modelBuilder.Entity<Course>().HasMany(c => c.Notices).WithRequired(n => n.Course).WillCascadeOnDelete(false);
            modelBuilder.Entity<Course>().HasMany(c => c.Lectures).WithRequired(n => n.Course).WillCascadeOnDelete(false);
            modelBuilder.Entity<Course>().HasMany(c => c.Assignments).WithRequired(n => n.Course).WillCascadeOnDelete(false);
            modelBuilder.Entity<Course>().HasMany(c => c.Exams).WithRequired(n => n.Course).WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseGrade>().HasRequired(x => x.Course);
            modelBuilder.Entity<CourseGrade>().HasMany(x => x.CourseWorkGrades).WithRequired(x => x.CourseGrade);
            modelBuilder.Entity<CourseWorkGrade>().HasRequired(x => x.CourseWork);

            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
