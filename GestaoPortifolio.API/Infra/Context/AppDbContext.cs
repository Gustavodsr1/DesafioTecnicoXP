using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>();
            modelBuilder.Entity<Oferta>();
            modelBuilder.Entity<Cliente>();
            modelBuilder.Entity<Operacao>();
            modelBuilder.Entity<Investimentos>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Job>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
