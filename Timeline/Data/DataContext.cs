#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timeline.Models;

namespace Timeline.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<timeline> timeline { get; set; }
        public virtual DbSet<timeline_context> timeline_context { get; set; }
        public virtual DbSet<timeline_context_tags> timeline_context_tags { get; set; }
        public virtual DbSet<users> users { get; set; }
        protected override void OnModelCreating(ModelBuilder build){
            build.Entity<timeline_context_tags>().HasKey(x=> new {x.timec_id,x.timec_tags});
        }
    }
}
