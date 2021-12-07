using Microsoft.EntityFrameworkCore;
using EFTest.Entities;

namespace EFTest.Context
{
    class StudentsDB : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        public StudentsDB(DbContextOptions<StudentsDB> options) : base(options) { }
    }
}
