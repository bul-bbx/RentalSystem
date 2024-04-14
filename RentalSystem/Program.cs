using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Data;
using System.Diagnostics;
using System;
using RentalSystem.Data.Entities;
using Microsoft.AspNetCore.Hosting;

namespace RentalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>();
                });
    }
}
