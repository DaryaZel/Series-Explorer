using System.Data.Entity;
using Series.Api.Data;
using Series.Api.Migrations;

namespace Series.Api
{
    public static class DatabaseConfig
    {
        public static void Register()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<SeriesDbContext, Configuration>());
        }
    }
}
