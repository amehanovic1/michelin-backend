using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Michelin.Models;
using Michelin.Util;

namespace Michelin.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Michelin.Models.Korisnik> Korisnik { get; set; }
        public DbSet<Michelin.Models.Sastojak> Sastojak { get; set; }
        public DbSet<Michelin.Models.Recept> Recept { get; set; }
        public DbSet<Michelin.Models.Komentar> Komentar { get; set; }
        public DbSet<Michelin.Models.Ocjena> Ocjena { get; set; }
        public DbSet<Michelin.Models.NacinPripreme> NacinPripreme { get; set; }
        public DbSet<Michelin.Models.ZahtjevZaPomoc> ZahtjevZaPomoc { get; set; }
        public DbSet<Michelin.Util.NajboljiRecept> NajboljiRecept { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Korisnik>().ToTable("Korisnik");
            builder.Entity<Sastojak>().ToTable("Sastojak");
            builder.Entity<Recept>().ToTable("Recept");
            builder.Entity<Komentar>().ToTable("Komentar");
            builder.Entity<Ocjena>().ToTable("Ocjena");
            builder.Entity<NacinPripreme>().ToTable("Nacin Pripreme");
            builder.Entity<ZahtjevZaPomoc>().ToTable("ZahtjevZaPomoc");
            builder.Entity<NajboljiRecept>().ToTable("NajboljiRecepti");
        }
    }
}
