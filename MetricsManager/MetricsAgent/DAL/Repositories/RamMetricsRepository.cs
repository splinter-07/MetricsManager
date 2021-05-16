using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHalder());
        }

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<RamMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics").ToList();
            }
        }

        public IList<RamMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics where time >= @fromTime and time <= @toTime",
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
