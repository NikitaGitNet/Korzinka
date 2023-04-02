using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorzinkaTZ.Model
{
    public class DataBaseContext : DbContext
    {
        public DbSet<GuidProduct> GuidProducts { get; set; } = default!;
        public DbSet<HistoryProduct> HistoryProducts { get; set; } = default!;
        public DbSet<HistoryPurchase> HistoryPurchases { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=1111;database=mydb;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
