using Microsoft.EntityFrameworkCore;
using Parking_App.Entities;

namespace Parking_App
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		override protected void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<TipoVehiculo>().Property(x => x.Tarifa).HasPrecision(4, 2);
			modelBuilder.Entity<TipoVehiculo>().HasData(
				new TipoVehiculo { Id = 1, Nombre = "Oficial", Tarifa = 0 },
				new TipoVehiculo { Id = 2, Nombre = "Residente", Tarifa = 0.05M },
				new TipoVehiculo { Id = 3, Nombre = "No Residente", Tarifa = 0.5M }
			);
			modelBuilder.Entity<Ticket>().Property(x => x.ImporteCobrado).HasPrecision(4, 2);
			

		}
        public  DbSet<TipoVehiculo> TipoVehiculos { get; set; }
		public  DbSet<Vehiculo> Vehiculos { get; set; }
		public  DbSet<Estancia> Estancias { get; set; }
		public  DbSet<Ticket> Ticket { get; set; }

    }
}
