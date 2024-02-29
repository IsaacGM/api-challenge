using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace ApiApplication.Database
{
    public class DataJobsContext : DbContext
    {
        public DataJobsContext(DbContextOptions<DataJobsContext> options) : base(options){}

        public DbSet<DataJobEntity> DataJobs { get; set; }
        public DbSet<DataJobStatusEntity> DataJobStatuses { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataJobStatusEntity>(build =>
            {
                build.HasKey(status => status.Id);
                build.Property(status => status.Id).ValueGeneratedOnAdd();

                modelBuilder.Entity<DataJobStatusEntity>()
                    .HasMany(status => status.DataJobs)
                    .WithOne(dataJob => dataJob.Status)
                    .HasForeignKey(dataJob => dataJob.StatusId);
            });

            modelBuilder.Entity<ResultEntity>(build => 
            {
                build.HasKey(result => result.Id);
                build.Property(result => result.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DataJobEntity>(build =>
            {
                build.HasKey(job => job.Id);
                
                modelBuilder.Entity<DataJobEntity>()
                    .HasMany(job => job.Results)
                    .WithOne()
                    .HasForeignKey(job => job.DataJobId);

                modelBuilder.Entity<DataJobEntity>()
                    .HasMany(job => job.Results)
                    .WithOne()
                    .HasForeignKey(result => result.DataJobId);
            });
        }
    }
}
