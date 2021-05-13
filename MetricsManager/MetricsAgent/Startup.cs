using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.DAL;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();

        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                //cpumetrics
                {
                    command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(01,1620801171)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(02,1620802172)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(03,1620803174)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(04,1620804176)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(05,1620805175)";
                    command.ExecuteNonQuery();
                }
                //rammetrics
                {
                    command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(06,1620806171)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(07,1620807172)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(08,1620808174)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(09,1620809176)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(10,1620810175)";
                    command.ExecuteNonQuery();
                }
                //networkmetrics
                {
                    command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(11,1620811171)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(12,1620812172)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(13,1620813174)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(14,1620814176)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(15,1620815175)";
                    command.ExecuteNonQuery();
                }
                //dotnetmetrics
                {
                    command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(16,1620816171)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(17,1620817172)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(18,1620818174)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(19,1620819176)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(20,1620820175)";
                    command.ExecuteNonQuery();
                }
                //hddmetrics
                {
                    command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(21,1620821171)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(22,1620822172)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(23,1620823174)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(24,1620824176)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(25,1620825175)";
                    command.ExecuteNonQuery();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
