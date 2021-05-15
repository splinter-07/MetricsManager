using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                { 
                    cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";

                    cmd.Parameters.AddWithValue("@value", item.Value);
                    cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
                    
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            { 
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                { 
                    cmd.CommandText = "SELECT * FROM cpumetrics";

                    var returnList = new List<CpuMetric>();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnList.Add(new CpuMetric
                            {
                                Id = reader.GetInt32(0),
                                Value = reader.GetInt32(1),
                                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                            });
                        }
                    }
                    return returnList;
                }
            }
        }

        public IList<CpuMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "SELECT * FROM cpumetrics where time >= @fromTime and time <= @toTime";
                    cmd.Parameters.AddWithValue("@fromTime", period.From.ToUnixTimeSeconds());
                    cmd.Parameters.AddWithValue("@toTime", period.To.ToUnixTimeSeconds());

                    var returnList = new List<CpuMetric>();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnList.Add(new CpuMetric
                            {
                                Id = reader.GetInt32(0),
                                Value = reader.GetInt32(1),
                                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                            });
                        }
                    }
                    return returnList;
                }
            }
        }
    }
}
