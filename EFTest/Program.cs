using Microsoft.EntityFrameworkCore;
using EFTest.Context;
using Microsoft.Extensions.Configuration;

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

        }
    }
}
