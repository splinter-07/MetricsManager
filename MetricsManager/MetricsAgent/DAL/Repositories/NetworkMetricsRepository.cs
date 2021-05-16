using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {

    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHalder());
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics").ToList();
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics where time >= @fromTime and time <= @toTime",
                    new
                    {
                        fromTime = period.From.ToUnixTimeSeconds(),
                        toTime = period.To.ToUnixTimeSeconds()
                    }
                    ).ToList();
            }
        }
    }
}
