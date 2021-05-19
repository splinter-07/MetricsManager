using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {

    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHalder());
        }
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }            
        }

        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics").ToList();
            }
        }

        public IList<DotNetMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics where time >= @fromTime and time <= @toTime",
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
