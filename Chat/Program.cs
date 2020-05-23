using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chat.Logging;
using Chat.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
            
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new FileLoggerProvider("log.txt"));
            }));

            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            using (ChatContext db = new ChatContext(options))
            {
                var users = db.Users.ToList();

                db.Users.Add(new User
                {
                    Firstname = "TestFN",
                    Lastname = "LName"
                });

                db.Users.Add(new User
                {
                    Firstname = "TestFN2",
                    Lastname = "LName2"
                });

                db.SaveChanges();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
