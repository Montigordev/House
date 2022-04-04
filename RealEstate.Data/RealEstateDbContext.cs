using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Domain;

namespace RealEstate.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options) { }

        public DbSet<House> House { get; set; }
        public DbSet<ExistingFilePath> ExistingFilePath { get; set; }
    }
}
