using System.Data.Entity;
using Models;

namespace Data
{
    public interface ISpeedyDonkeyDbContext
    {
        IDbSet<User> Users { get; set; }

        int SaveChanges();
        void SetEntityState(object entity, EntityState state);

        IDbSet<TEntity> GetSetOfType<TEntity>() where TEntity : class;
    }

    public class SpeedyDonkeyDbContext : DbContext, ISpeedyDonkeyDbContext
    {
        public IDbSet<User> Users { get; set; }

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
        //    modelBuilder.Entity<Assignment>().ToTable("Assignment");
        //    modelBuilder.Entity<Exam>().ToTable("Exam");

        //    //Not mapping as expected? Are you including the property in the repo?!

        //    //0-1 -> 1
            //modelBuilder.Entity<Account>().HasOptional(p => p.User).WithRequired(l => (Account) l.Account);

        //    //0-* -> 1-*
        //    modelBuilder.Entity<Professor>().HasMany(p => p.Courses).WithMany(c => c.Professors);

        //    //* - 1
        //    modelBuilder.Entity<Course>().HasMany(c => c.Notices).WithRequired(n => n.Course).WillCascadeOnDelete(false);
        //    modelBuilder.Entity<Course>().HasMany(c => c.Lectures).WithRequired(n => n.Course).WillCascadeOnDelete(false);
        //    modelBuilder.Entity<Course>().HasMany(c => c.Assignments).WithRequired(n => n.Course).WillCascadeOnDelete(false);
        //    modelBuilder.Entity<Course>().HasMany(c => c.Exams).WithRequired(n => n.Course).WillCascadeOnDelete(false);

        //    modelBuilder.Entity<CourseGrade>().HasRequired(x => x.Course);
        //    modelBuilder.Entity<CourseGrade>().HasMany(x => x.CourseWorkGrades).WithRequired(x => x.CourseGrade);
        //    modelBuilder.Entity<CourseWorkGrade>().HasRequired(x => x.CourseWork);

        //    base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
