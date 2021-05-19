using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHalder());
        }

        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics").ToList();
            }
        }

        public IList<HddMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics where time >= @fromTime and time <= @toTime",
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
