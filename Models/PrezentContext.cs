using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZadanieApi.Models;


namespace ZadanieApi.Models
{
    public class PrezentContext : DbContext
    {
        public PrezentContext(DbContextOptions<PrezentContext> options)
            : base(options)
        {

        }
        public DbSet<Prezent> Prezents { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Prezent>().HasData(
        //        new Prezent
        //         {
        //            Id = 1, NazwaPrezentu = "Lalka", CenaPrezentu = 100, KategoriaPrezentu = "Zabawka", WiekPrezentu = "1-10"
        //         }
        //        );
        //}
    }
}
