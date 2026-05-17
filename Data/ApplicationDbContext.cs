using Lab_System.Models;
using Lab_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options ) : base( options ) { }

        public DbSet<Chemicals> Chemicals { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<ReactionStep> ReactionSteps { get; set; }
        public DbSet<ResearcherProfile> ResearcherProfiles { get; set; }
        public DbSet<ExperimentChemical> ExperimentChemicals { get; set; }

        public DbSet<ExperimentAttachment> ExperimentAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Chemicals>().ToTable("Chemicals");
            builder.Entity<Equipment>().ToTable("Equipment");
            builder.Entity<Experiment>().ToTable("Experiment");
            builder.Entity<ReactionStep>().ToTable("ReactionStep");
            builder.Entity<ResearcherProfile>().ToTable("ResearcherProfile");
            builder.Entity<ExperimentChemical>().ToTable("ExperimentChemical");
            builder.Entity<ExperimentAttachment>().ToTable("ExperimentAttachment");
        }
    }
}
