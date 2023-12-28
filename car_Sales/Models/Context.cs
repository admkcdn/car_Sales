using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace car_Sales.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected Context()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("server=LAPTOP-BMW\\SQLEXPRESS; database=carSalesSite; integrated security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("server=LAPTOP-BMW\\SQLEXPRESS; database=CarSale; integrated security=true;");
        //}

        public DbSet<Cars> cars { get; set; }
        public DbSet<CarStatus> carStatuses { get; set; }
        public DbSet<Types> types { get; set; }
        public DbSet<CarParts> CarParts { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<PriceTransactions> priceTransactions { get; set; }
        public DbSet<TransmissionType> transmissionTypes { get; set; }
        public DbSet<Image> ımages { get; set; }
        public DbSet<FuelType> fuelTypes { get; set; }
        public DbSet<Advert> adverts { get; set; }
        public DbSet<Users> users { get; set; }
    }
}
