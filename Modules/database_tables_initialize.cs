using Tables;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace database_rules
{
    public class Database : DbContext
    {

        public DbSet<Template> Template { get; set; }
        public DbSet<Question_table> Question_Table { get; set; }
        public DbSet<option_table> option_Table{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=QUESTIONS_GENERATED_Mock4;Trusted_Connection=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
         modelBuilder.Entity<Template>().HasMany(n => n.questions_topic).WithOne().HasForeignKey(c => c.template_id);
         modelBuilder.Entity<Question_table>().HasMany(n => n.options).WithOne().HasForeignKey(c => c.question_id);
        }

    }
}