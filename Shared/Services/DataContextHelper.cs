using Microsoft.EntityFrameworkCore;
using projectWEB.Data;


namespace projectWEB.Services
{
    public static class DataContextHelper
    {
        private static string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=projectWEBContext-41c7de9e-2271-4758-ab70-3722452649e9;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static DbContextOptions<projectWEBContext> GetNewContext()
        {
            return new DbContextOptionsBuilder<projectWEBContext>()
                              .UseSqlServer(_connectionString).Options;
        }
    }
}
