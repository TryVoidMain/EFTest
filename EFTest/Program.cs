using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using EFTest.Context;
using EFTest.Entities;

namespace EFTest
{
    class Program
    {
        static void Main()
        {
            const string sql_server_connection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.Test.db";

            var connection_options = new DbContextOptionsBuilder<StudentsDB>()
                .UseSqlServer(sql_server_connection_string)
                .Options;
            using (var db = new StudentsDB(connection_options))
            {
                db.Database.EnsureCreated();

                if(!db.Students.Any())
                {
                    var ivanov = new Student
                    {
                        LastName = "Иванов",
                        Name = "Иван",
                        Patronymic = "Иванович",
                        Birthday = DateTime.Now.AddYears(-21),
                    };

                    var petrov = new Student
                    {
                        LastName = "Петров",
                        Name = "Пётр",
                        Patronymic = "Петрович",
                        Birthday = DateTime.Now.AddYears(-24),
                    };

                    var sidorov = new Student
                    {
                        LastName = "Сидоров",
                        Name = "Сидор",
                        Patronymic = "Сидорович",
                        Birthday = DateTime.Now.AddYears(-22),
                    };

                    var group1 = new Group() { Name = "Группа 1" };
                    var group2 = new Group() { Name = "Группа 2" };

                    group1.Students.Add(ivanov);
                    group1.Students.Add(petrov);

                    group2.Students.Add(sidorov);

                    db.Groups.Add(group1);
                    db.Groups.Add(group2);

                    db.SaveChanges();
                }
            }
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var db_options = new DbContextOptionsBuilder<StudentsDB>()
                .UseSqlServer(configuration.GetConnectionString("Default"))
                .Options;

            using (var db = new StudentsDB(db_options))
            {
                var groups_with_max_students = db.Groups
                    .Include(g => g.Students)
                    .OrderByDescending(g => g.Students.Count)
                    .First();

                Console.WriteLine(groups_with_max_students.Name);
                foreach (var students in groups_with_max_students.Students)
                    Console.WriteLine("{0} {1} {2}", students.LastName, students.Name, students.Patronymic);
            }
                                



        }
    }
}
