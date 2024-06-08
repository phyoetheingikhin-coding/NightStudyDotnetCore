using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NightStudyDotNetCore.MVCApp.Models;
using System.Data.SqlClient;

namespace NightStudyDotNetCore.MVCApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource=".\\MSSQLSERVER1",
                InitialCatalog ="TestDb",
                UserID="sa",
                Password="phyo@123",
                TrustServerCertificate=true

            };
            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<CustomerModel> Customers { get; set; }
    }
}
