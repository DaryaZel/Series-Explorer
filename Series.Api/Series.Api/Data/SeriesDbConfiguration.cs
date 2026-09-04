using System.Data.Entity;
using Npgsql;

namespace Series.Api.Data
{
    public class SeriesDbConfiguration : DbConfiguration
    {
        public SeriesDbConfiguration()
        {
            const string ProviderName = "Npgsql";

            SetProviderFactory(ProviderName, NpgsqlFactory.Instance);
            SetProviderServices(ProviderName, NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }
}
