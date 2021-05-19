using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.Models;
using Dapper;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHalder());
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics").ToList();
            }
        }

        public IList<CpuMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(Connection.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics where time >= @fromTime and time <= @toTime",
                    new { 
                            fromTime = period.From.ToUnixTimeSeconds(),
                            toTime = period.To.ToUnixTimeSeconds()
                        }
                    ).ToList();
            }
        }
    }
}
