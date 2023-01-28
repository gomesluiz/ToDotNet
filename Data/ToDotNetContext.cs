using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Models;

namespace ToDotNet.Data
{
    public class ToDotNetContext : DbContext
    {
        public ToDotNetContext (DbContextOptions<ToDotNetContext> options)
            : base(options)
        {

        }

        public DbSet<ToDotNet.Models.User> User { get; set; } = default!;

        public DbSet<ToDotNet.Models.Todo> Todo { get; set; } = default!;
    }
}
