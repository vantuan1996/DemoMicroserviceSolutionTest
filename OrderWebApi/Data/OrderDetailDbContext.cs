using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OrderWebApi.Models;

namespace OrderWebApi.Data
{
    public class OrderDetailDbContext :DbContext
    {
        public OrderDetailDbContext(DbContextOptions<OrderDetailDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

      
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
