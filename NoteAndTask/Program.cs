﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NoteAndTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}