using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Series.Api.Models;

namespace Series.Api.Data
{
    [DbConfigurationType(typeof(SeriesDbConfiguration))]
    public class SeriesDbContext : DbContext
    {
        public SeriesDbContext()
            : base("name=SeriesDb")
        {
        }

        public DbSet<SeriesRecord> Series { get; set; }

        public DbSet<HierarchyRecord> Hierarchy { get; set; }

        public DbSet<ObservationRecord> Observations { get; set; }

        public DbSet<LockedPeriodRecord> LockedPeriods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeriesRecord>()
                .ToTable("tblSeries")
                .HasKey(series => series.Id);

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Id)
                .HasColumnName("KY_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Description)
                .HasColumnName("TX_DESCRIPTION")
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Sku)
                .HasColumnName("TX_SKU")
                .HasMaxLength(50);

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Country)
                .HasColumnName("TX_COUNTRY")
                .HasMaxLength(50);

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Customer)
                .HasColumnName("TX_CUSTOMER")
                .HasMaxLength(50);

            modelBuilder.Entity<SeriesRecord>()
                .Property(series => series.Category)
                .HasColumnName("TX_CATEGORY")
                .HasMaxLength(50);

            modelBuilder.Entity<HierarchyRecord>()
                .ToTable("tblHierarchy")
                .HasKey(hierarchy => hierarchy.SeriesId);

            modelBuilder.Entity<HierarchyRecord>()
                .Property(hierarchy => hierarchy.SeriesId)
                .HasColumnName("FK_SERIES")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<HierarchyRecord>()
                .Property(hierarchy => hierarchy.ParentSeriesId)
                .HasColumnName("FK_PARENT_SERIES");

            modelBuilder.Entity<ObservationRecord>()
                .ToTable("tblObservations")
                .HasKey(observation => new { observation.SeriesId, observation.Period });

            modelBuilder.Entity<ObservationRecord>()
                .Property(observation => observation.SeriesId)
                .HasColumnName("FK_SERIES");

            modelBuilder.Entity<ObservationRecord>()
                .Property(observation => observation.Period)
                .HasColumnName("TX_PERIOD")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ObservationRecord>()
                .Property(observation => observation.Sales)
                .HasColumnName("QY_SALES");

            modelBuilder.Entity<ObservationRecord>()
                .Property(observation => observation.Demand)
                .HasColumnName("QY_DEMAND");

            modelBuilder.Entity<ObservationRecord>()
                .Property(observation => observation.Supply)
                .HasColumnName("QY_SUPPLY");

            modelBuilder.Entity<LockedPeriodRecord>()
                .ToTable("tblLockedPeriods")
                .HasKey(lockedPeriod => new
                {
                    lockedPeriod.SeriesId,
                    lockedPeriod.Period,
                    lockedPeriod.Opinion
                });

            modelBuilder.Entity<LockedPeriodRecord>()
                .Property(lockedPeriod => lockedPeriod.SeriesId)
                .HasColumnName("FK_SERIES");

            modelBuilder.Entity<LockedPeriodRecord>()
                .Property(lockedPeriod => lockedPeriod.Period)
                .HasColumnName("TX_PERIOD")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<LockedPeriodRecord>()
                .Property(lockedPeriod => lockedPeriod.Opinion)
                .HasColumnName("TX_OPINION")
                .HasMaxLength(50)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
