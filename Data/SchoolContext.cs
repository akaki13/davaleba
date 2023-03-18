using Microsoft.EntityFrameworkCore;
using testi.Models;

namespace testi.Data
{
    public class SchoolContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Scool.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherPupil>().HasKey(tp => new { tp.TeacherId, tp.PupilId });

            modelBuilder.Entity<TeacherPupil>()
                .HasOne(i => i.Teacher)
                .WithMany(i => i.TeacherPupils)
                .HasForeignKey(i => i.TeacherId);

            modelBuilder.Entity<TeacherPupil>()
                .HasOne(i => i.Pupil)
                .WithMany(i => i.TeacherPupils)
                .HasForeignKey(i => i.PupilId);
            
            modelBuilder.Entity<TeacherPupil>().HasData(
                new TeacherPupil
                {
                    TeacherId = 1,
                    PupilId = 1
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "nami",
                    LastName = "cat",
                    Gender = "f",
                    Subject = "math"
                }
            );

            modelBuilder.Entity<Pupil>().HasData(
                new Pupil
                {
                    Id = 1,
                    FirstName = "zoro",
                    LastName = "roronoa",
                    Gender = "m",
                    ClassName = "6b"

                }
            );
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
    }
}