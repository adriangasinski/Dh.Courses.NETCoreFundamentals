using Microsoft.EntityFrameworkCore;
using NoweM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoweM.Data
{
    public class NoweMDbContext : DbContext
    {
        public NoweMDbContext(DbContextOptions<NoweMDbContext> options)
            :base(options)
        {

        }
        public DbSet<House> Houses { get; set; }

    }
}
