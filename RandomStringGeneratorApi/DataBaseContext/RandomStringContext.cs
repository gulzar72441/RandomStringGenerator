using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomStringGeneratorApi.Models;

namespace RandomStringGeneratorApi.DataBaseContext
{
    public class RandomStringContext : DbContext
    {
        public RandomStringContext(DbContextOptions<RandomStringContext> options) : base(options) { }
       
        public DbSet<RandomString> RandomString { get; set; }

    }

}
