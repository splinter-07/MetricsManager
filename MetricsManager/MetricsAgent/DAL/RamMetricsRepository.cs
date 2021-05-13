using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                { 
                    cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";

                    cmd.Parameters.AddWithValue("@value", item.Value);
                    cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());

                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<RamMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                { 
                    cmd.CommandText = "SELECT * FROM rammetrics";

                    var returnList = new List<RamMetric>();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnList.Add(new RamMetric
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

        public IList<RamMetric> GetByTimePeriod(TimePeriod period)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "SELECT * FROM rammetrics where time >= @fromTime and time <= @toTime";
                    cmd.Parameters.AddWithValue("@fromTime", period.From.ToUnixTimeSeconds());
                    cmd.Parameters.AddWithValue("@toTime", period.To.ToUnixTimeSeconds());

                    var returnList = new List<RamMetric>();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnList.Add(new RamMetric
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
