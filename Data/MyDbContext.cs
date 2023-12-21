using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PISIAssessment.Model;
using static PISIAssessment.Data.MyDbContext;

namespace PISIAssessment.Data
{
    public class MyDbContext : DbContext
    {
    public MyDbContext (DbContextOptions<MyDbContext> options) : base(options)
    { 
    }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Service>()
        .HasOne(e => e.subscriber)
        .WithOne(e => e.service);
        
}
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Subscriber> Subscribers => Set<Subscriber>();
    }

   
}