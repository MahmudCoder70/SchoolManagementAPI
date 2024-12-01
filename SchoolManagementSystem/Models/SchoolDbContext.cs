using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Models
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingRoom> BuildingRooms { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<CampusClass>  CampusClasses { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<CampusClassSection> CampusClassSections { get; set; }
        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolType> SchoolsType { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCampusClassSection> StudentCampusClassSections { get; set; }
        public DbSet<StudentGuardian> StudentGuardian { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeachersSubject { get; set; }
        public DbSet<TeacherClass> TeacherClass { get; set; }
        public DbSet<RoutineConfiguration> RoutineConfigurations { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }

    }
}
