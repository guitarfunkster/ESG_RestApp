using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ESG_RestApp.Models;

namespace ESG_RestApp.Data
{
    public class ESG_RestAppContext : DbContext
    {
        public ESG_RestAppContext (DbContextOptions<ESG_RestAppContext> options)
            : base(options)
        {
        }

        public DbSet<ESG_RestApp.Models.Customer> Customer { get; set; } = default!;
    }
}
