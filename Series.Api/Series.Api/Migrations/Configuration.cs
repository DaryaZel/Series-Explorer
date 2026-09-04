using System.Data.Entity.Migrations;
using Npgsql;
using Series.Api.Data;
using Series.Api.Models;

namespace Series.Api.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SeriesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "Series.Api.Data.SeriesDbContext";

            SetSqlGenerator("Npgsql", new NpgsqlMigrationSqlGenerator());
        }

        protected override void Seed(SeriesDbContext context)
        {
            context.Series.AddOrUpdate(
                series => series.Id,
                new SeriesRecord { Id = 1, Description = "NORTH AMERICA", Country = "NORTH AMERICA" },
                new SeriesRecord { Id = 2, Description = "URBAN RETAIL GROUP", Country = "US", Customer = "URBAN RETAIL GROUP" },
                new SeriesRecord { Id = 3, Description = "S4248", Sku = "S4248", Country = "US", Customer = "URBAN RETAIL GROUP", Category = "HOME" },
                new SeriesRecord { Id = 4, Description = "S5172", Sku = "S5172", Country = "US", Customer = "URBAN RETAIL GROUP", Category = "HOME" },
                new SeriesRecord { Id = 5, Description = "S6390", Sku = "S6390", Country = "US", Customer = "URBAN RETAIL GROUP", Category = "OUTDOOR" },
                new SeriesRecord { Id = 6, Description = "MARKET LANE STORES", Country = "US", Customer = "MARKET LANE STORES" },
                new SeriesRecord { Id = 7, Description = "S2841", Sku = "S2841", Country = "US", Customer = "MARKET LANE STORES", Category = "PANTRY" },
                new SeriesRecord { Id = 8, Description = "S7753", Sku = "S7753", Country = "US", Customer = "MARKET LANE STORES", Category = "WELLNESS" },
                new SeriesRecord { Id = 9, Description = "EUROPE", Country = "EUROPE" },
                new SeriesRecord { Id = 10, Description = "NORDIC WHOLESALE", Country = "SWEDEN", Customer = "NORDIC WHOLESALE" },
                new SeriesRecord { Id = 11, Description = "S9035", Sku = "S9035", Country = "SWEDEN", Customer = "NORDIC WHOLESALE", Category = "COLD STORAGE" },
                new SeriesRecord { Id = 12, Description = "S1186", Sku = "S1186", Country = "SWEDEN", Customer = "NORDIC WHOLESALE", Category = "SMART HOME" },
                new SeriesRecord { Id = 13, Description = "APAC", Country = "APAC" },
                new SeriesRecord { Id = 14, Description = "HARBOR ONLINE", Country = "SINGAPORE", Customer = "HARBOR ONLINE" },
                new SeriesRecord { Id = 15, Description = "S6427", Sku = "S6427", Country = "SINGAPORE", Customer = "HARBOR ONLINE", Category = "MOBILE ACCESSORIES" });

            context.SaveChanges();

            context.Hierarchy.AddOrUpdate(
                hierarchy => hierarchy.SeriesId,
                new HierarchyRecord { SeriesId = 1, ParentSeriesId = 0 },
                new HierarchyRecord { SeriesId = 2, ParentSeriesId = 1 },
                new HierarchyRecord { SeriesId = 3, ParentSeriesId = 2 },
                new HierarchyRecord { SeriesId = 4, ParentSeriesId = 2 },
                new HierarchyRecord { SeriesId = 5, ParentSeriesId = 2 },
                new HierarchyRecord { SeriesId = 6, ParentSeriesId = 1 },
                new HierarchyRecord { SeriesId = 7, ParentSeriesId = 6 },
                new HierarchyRecord { SeriesId = 8, ParentSeriesId = 6 },
                new HierarchyRecord { SeriesId = 9, ParentSeriesId = 0 },
                new HierarchyRecord { SeriesId = 10, ParentSeriesId = 9 },
                new HierarchyRecord { SeriesId = 11, ParentSeriesId = 10 },
                new HierarchyRecord { SeriesId = 12, ParentSeriesId = 10 },
                new HierarchyRecord { SeriesId = 13, ParentSeriesId = 0 },
                new HierarchyRecord { SeriesId = 14, ParentSeriesId = 13 },
                new HierarchyRecord { SeriesId = 15, ParentSeriesId = 14 });

            context.SaveChanges();

            context.Observations.AddOrUpdate(
                observation => new { observation.SeriesId, observation.Period },
                new ObservationRecord { SeriesId = 2, Period = "2023-01", Sales = 23680.00, Demand = 21150.00, Supply = 20420.00 },
                new ObservationRecord { SeriesId = 2, Period = "2024-01", Sales = 24940.00, Demand = 22280.00, Supply = 21495.00 },
                new ObservationRecord { SeriesId = 2, Period = "2025-01", Sales = 26300.00, Demand = 23100.00, Supply = 22400.00 },
                new ObservationRecord { SeriesId = 3, Period = "2023-03", Sales = 4525.00, Demand = 4160.00, Supply = 3985.00 },
                new ObservationRecord { SeriesId = 3, Period = "2024-03", Sales = 4740.00, Demand = 4385.00, Supply = 4215.00 },
                new ObservationRecord { SeriesId = 3, Period = "2025-03", Sales = 4980.00, Demand = null, Supply = 4390.00 },
                new ObservationRecord { SeriesId = 4, Period = "2020-01", Sales = 5120.50, Demand = 4800.00, Supply = 4600.00 },
                new ObservationRecord { SeriesId = 4, Period = "2020-04", Sales = 5485.75, Demand = 5010.00, Supply = 4825.00 },
                new ObservationRecord { SeriesId = 4, Period = "2020-07", Sales = 5790.00, Demand = null, Supply = 5100.00 },
                new ObservationRecord { SeriesId = 4, Period = "2020-10", Sales = 6025.25, Demand = 5480.00, Supply = 5335.00 },
                new ObservationRecord { SeriesId = 4, Period = "2021-01", Sales = 6310.00, Demand = 5650.00, Supply = 5480.00 },
                new ObservationRecord { SeriesId = 4, Period = "2021-04", Sales = 6625.40, Demand = 5885.00, Supply = 5715.00 },
                new ObservationRecord { SeriesId = 4, Period = "2021-07", Sales = 6900.85, Demand = 6120.00, Supply = 5960.00 },
                new ObservationRecord { SeriesId = 4, Period = "2021-10", Sales = 7245.10, Demand = 6405.00, Supply = 6230.00 },
                new ObservationRecord { SeriesId = 4, Period = "2022-01", Sales = 7520.00, Demand = 6680.00, Supply = 6490.00 },
                new ObservationRecord { SeriesId = 4, Period = "2022-04", Sales = 7895.35, Demand = 6950.00, Supply = null },
                new ObservationRecord { SeriesId = 4, Period = "2022-07", Sales = 8150.75, Demand = 7215.00, Supply = 7010.00 },
                new ObservationRecord { SeriesId = 4, Period = "2022-10", Sales = 8465.00, Demand = 7500.00, Supply = 7280.00 },
                new ObservationRecord { SeriesId = 4, Period = "2023-01", Sales = 8825.20, Demand = 7790.00, Supply = 7565.00 },
                new ObservationRecord { SeriesId = 4, Period = "2023-04", Sales = 9180.00, Demand = 8065.00, Supply = 7820.00 },
                new ObservationRecord { SeriesId = 4, Period = "2023-07", Sales = 9475.60, Demand = 8350.00, Supply = 8105.00 },
                new ObservationRecord { SeriesId = 4, Period = "2023-10", Sales = 9840.30, Demand = 8625.00, Supply = 8380.00 },
                new ObservationRecord { SeriesId = 4, Period = "2024-01", Sales = 10125.00, Demand = 8910.00, Supply = 8650.00 },
                new ObservationRecord { SeriesId = 4, Period = "2024-04", Sales = null, Demand = 9250.00, Supply = 8965.00 },
                new ObservationRecord { SeriesId = 4, Period = "2024-07", Sales = 10875.75, Demand = 9580.00, Supply = 9300.00 },
                new ObservationRecord { SeriesId = 4, Period = "2024-10", Sales = 11240.00, Demand = 9895.00, Supply = 9625.00 },
                new ObservationRecord { SeriesId = 4, Period = "2025-01", Sales = 11610.50, Demand = 10250.00, Supply = 9940.00 },
                new ObservationRecord { SeriesId = 4, Period = "2025-04", Sales = 11985.25, Demand = 10610.00, Supply = 10290.00 },
                new ObservationRecord { SeriesId = 4, Period = "2025-07", Sales = 12340.00, Demand = 10975.00, Supply = 10640.00 },
                new ObservationRecord { SeriesId = 4, Period = "2025-10", Sales = 12725.80, Demand = 11350.00, Supply = 11005.00 },
                new ObservationRecord { SeriesId = 5, Period = "2023-03", Sales = 4520.00, Demand = 4210.00, Supply = 3980.00 },
                new ObservationRecord { SeriesId = 5, Period = "2024-03", Sales = 4765.00, Demand = 4435.00, Supply = 4210.00 },
                new ObservationRecord { SeriesId = 5, Period = "2025-03", Sales = 5010.00, Demand = 4660.00, Supply = null },
                new ObservationRecord { SeriesId = 7, Period = "2023-06", Sales = 9460.00, Demand = 8840.00, Supply = 8515.00 },
                new ObservationRecord { SeriesId = 7, Period = "2024-06", Sales = 9955.00, Demand = 9295.00, Supply = 8920.00 },
                new ObservationRecord { SeriesId = 7, Period = "2025-06", Sales = 10425.00, Demand = 9720.00, Supply = 9340.00 },
                new ObservationRecord { SeriesId = 8, Period = "2023-09", Sales = 4385.00, Demand = 4075.00, Supply = 3820.00 },
                new ObservationRecord { SeriesId = 8, Period = "2024-09", Sales = 4610.00, Demand = 4295.00, Supply = 4055.00 },
                new ObservationRecord { SeriesId = 8, Period = "2025-09", Sales = 4860.00, Demand = 4520.00, Supply = 4250.00 },
                new ObservationRecord { SeriesId = 9, Period = "2023-01", Sales = 23980.00, Demand = 22410.00, Supply = 21320.00 },
                new ObservationRecord { SeriesId = 9, Period = "2024-01", Sales = 25240.00, Demand = 23595.00, Supply = 22430.00 },
                new ObservationRecord { SeriesId = 9, Period = "2025-01", Sales = 26490.00, Demand = 24760.00, Supply = 23520.00 },
                new ObservationRecord { SeriesId = 10, Period = "2023-01", Sales = 14520.00, Demand = 13685.00, Supply = 13010.00 },
                new ObservationRecord { SeriesId = 10, Period = "2024-01", Sales = 15280.00, Demand = 14395.00, Supply = 13695.00 },
                new ObservationRecord { SeriesId = 10, Period = "2025-01", Sales = 16040.00, Demand = 15120.00, Supply = 14390.00 },
                new ObservationRecord { SeriesId = 11, Period = "2023-02", Sales = 7360.00, Demand = 6845.00, Supply = 6505.00 },
                new ObservationRecord { SeriesId = 11, Period = "2024-02", Sales = 7745.00, Demand = 7190.00, Supply = 6835.00 },
                new ObservationRecord { SeriesId = 11, Period = "2025-02", Sales = 8125.00, Demand = 7540.00, Supply = 7160.00 },
                new ObservationRecord { SeriesId = 12, Period = "2023-11", Sales = 5695.00, Demand = 5245.00, Supply = 4960.00 },
                new ObservationRecord { SeriesId = 12, Period = "2024-11", Sales = 5985.00, Demand = 5520.00, Supply = 5225.00 },
                new ObservationRecord { SeriesId = 12, Period = "2025-11", Sales = 6280.00, Demand = 5795.00, Supply = 5485.00 },
                new ObservationRecord { SeriesId = 14, Period = "2023-01", Sales = 12405.00, Demand = 11480.00, Supply = 10985.00 },
                new ObservationRecord { SeriesId = 14, Period = "2024-01", Sales = 13060.00, Demand = 12075.00, Supply = 11550.00 },
                new ObservationRecord { SeriesId = 14, Period = "2025-01", Sales = 13720.00, Demand = 12680.00, Supply = 12140.00 },
                new ObservationRecord { SeriesId = 15, Period = "2023-05", Sales = 12405.00, Demand = 11480.00, Supply = 10985.00 },
                new ObservationRecord { SeriesId = 15, Period = "2024-05", Sales = 13060.00, Demand = 12075.00, Supply = 11550.00 },
                new ObservationRecord { SeriesId = 15, Period = "2025-05", Sales = 13720.00, Demand = 12680.00, Supply = 12140.00 });

            context.SaveChanges();

            context.LockedPeriods.AddOrUpdate(
                lockedPeriod => new { lockedPeriod.SeriesId, lockedPeriod.Period, lockedPeriod.Opinion },
                new LockedPeriodRecord { SeriesId = 4, Period = "2020-04", Opinion = "QY_SALES" },
                new LockedPeriodRecord { SeriesId = 4, Period = "2021-07", Opinion = "QY_DEMAND" },
                new LockedPeriodRecord { SeriesId = 4, Period = "2022-10", Opinion = "QY_SUPPLY" },
                new LockedPeriodRecord { SeriesId = 4, Period = "2023-04", Opinion = "QY_SALES" },
                new LockedPeriodRecord { SeriesId = 4, Period = "2024-07", Opinion = "QY_DEMAND" },
                new LockedPeriodRecord { SeriesId = 4, Period = "2025-10", Opinion = "QY_SUPPLY" },
                new LockedPeriodRecord { SeriesId = 7, Period = "2025-06", Opinion = "QY_SALES" },
                new LockedPeriodRecord { SeriesId = 8, Period = "2025-09", Opinion = "QY_DEMAND" },
                new LockedPeriodRecord { SeriesId = 11, Period = "2025-02", Opinion = "QY_SUPPLY" },
                new LockedPeriodRecord { SeriesId = 15, Period = "2025-05", Opinion = "QY_SALES" });

            context.SaveChanges();
        }
    }
}
