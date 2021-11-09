using church_mgt_database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace church_mgt_api.extensions
{
    public static class ConnectionConfig
    {
        private static string ConnectHeroku()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            // parse the connection string
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
            $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";

        }

        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddDbContextPool<ChurchDbContext>(options =>
            {
                string connStr;

                if (env.IsProduction())
                {
                    connStr = ConnectHeroku();
                }
                else
                {
                    connStr = config.GetConnectionString("default");
                }
                options.UseNpgsql(connStr);
            });
        }
    }
}
