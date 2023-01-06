using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSistemaAlguel.Models;

namespace WebSistemaAlguel.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<Aluguel> alguel { get; set; }
        public DbSet<Carro> carro { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Fabricante> fabricantes { get; set; }
        public DbSet<Funcionario> funcionario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityRole>().HasData(new IdentityRole {
                Id = "123e4567-e89b-12d3-a456-426655440000",
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            });

            builder.Entity<Funcionario>().HasData(new Funcionario {
                Id = "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0",
                Email = "funcionarioADM@mail.com",
                UserName = "funcionarioADM@mail.com",
                NormalizedUserName = "FUNCIONARIOADM@MAIL.COM",
                NormalizedEmail = "FUNCIONARIOADM@MAIL.COM",
                PasswordHash = hasher.HashPassword(null, "Senha@123"),
                setor_funcionario = "Usuario ADM"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string> {
                        RoleId = "123e4567-e89b-12d3-a456-426655440000",
                        UserId = "1ee7cf0a0-1922-401b-a1ae-6ec9261484c0"
                    });
        }
    }
}