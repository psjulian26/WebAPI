using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Data
{
    public  class StudentsContext : DbContext
    {
        private readonly IConfiguration _configuration; 

        public StudentsContext(DbContextOptions options,IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  DbSet<AddressTbl> AddressTbl { get; set; }
        public  DbSet<ProfessorsTbl> ProfessorsTbl { get; set; }
        public  DbSet<StudentInfoTbl> StudentInfoTbl { get; set; }
        public  DbSet<StudentSubjectsTbl> StudentSubjectsTbl { get; set; }
        public  DbSet<SubjectsTbl> SubjectsTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("StudentsRepo"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            //modelBuilder.Entity<ProfessorsTbl>(entity =>
            //{
            //    entity.HasOne(d => d.Sub)
            //        .WithMany(p => p.ProfessorsTbl)
            //        .HasForeignKey(d => d.SubId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ProfessorsTbl_SubjectsTbl");
            //});

            //modelBuilder.Entity<StudentInfoTbl>(entity =>
            //{
            //    entity.HasOne(d => d.Add)
            //        .WithMany(p => p.StudentInfoTbl)
            //        .HasForeignKey(d => d.AddId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentInfoTbl_AddressTbl");
            //});

            //modelBuilder.Entity<StudentSubjectsTbl>(entity =>
            //{
            //    entity.HasOne(d => d.Sub)
            //        .WithMany(p => p.StudentSubjectsTbl)
            //        .HasForeignKey(d => d.SubId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentSubjectsTbl_SubjectsTbl");
            //});
        }
    }
}
