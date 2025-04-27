using Microsoft.EntityFrameworkCore;
using ProcessoLabSystem.Models;

namespace ProcessoLabSystem.Data
{
    public class ContextoBancoDeDados : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Faz com que o arquivo .db criado seja no modelo do Sqlite
            optionsBuilder.UseSqlite("Data Source=lab_system.db");
        }
    }
}