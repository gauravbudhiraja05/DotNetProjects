using System;
using System.IO;
using System.Net.Mail;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace PickfordsIntranet.WebAdmin
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var host = BuildWebHost(args);
        //    host.Run();
        //}

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .Build();

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            //var server = config["server"] ?? "Kestrel";

            var builder = new WebHostBuilder()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration() // This declaration is a no-op if not run under IIS so it does not prevent running in self-hosted mode.
                .UseStartup<Startup>()
                .UseKestrel();



            var host = builder.Build();
            host.Run();
        }
    }
}
