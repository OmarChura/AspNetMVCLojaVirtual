using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Database
{
    public class LojaVirtualContext : DbContext
    {
        /*
         * EF Core - ORM
         * ORM -> biblioteca mapear obtejos para o banco de dados relacionais
         */
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options) 
        { 
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<NewletterEmail> NewletterEmails { get; set; }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }

}
