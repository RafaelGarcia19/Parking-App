using Microsoft.EntityFrameworkCore;

namespace Parking_App
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
