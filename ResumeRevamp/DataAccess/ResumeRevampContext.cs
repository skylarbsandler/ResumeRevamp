using Microsoft.EntityFrameworkCore;
using ResumeRevamp.Models;

namespace ResumeRevamp.DataAccess
{
    public class ResumeRevampContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<User> Users { get; set; }

        public ResumeRevampContext(DbContextOptions<ResumeRevampContext> options)
            : base(options) { }
    }
}
