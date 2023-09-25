using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Data
{
    public class DbAddress : DbContext
    {
        public DbAddress(DbContextOptions<DbAddress> options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<User> Users => Set<User>();

        public object AddAddressRequestDTO { get; internal set; }

    }
}