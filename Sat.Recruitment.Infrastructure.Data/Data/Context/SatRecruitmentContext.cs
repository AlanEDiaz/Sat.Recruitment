using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Entities;
using FileContextCore;
using FileContextCore.FileManager;
using CsvHelper;
using FileContextCore.Serializer;
using System.IO;

namespace Sat.Recruitment.Infrastructure.Data.Context
{
    /// <summary>Db context for proyect, by now using a connection to excel CSV</summary>
    public class SatRecruitmentContext : DbContext
    {
        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseFileContextDatabase<CSVSerializer, DefaultFileManager>(location: Directory.GetCurrentDirectory() + "/Files/Users");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }
    }
}
