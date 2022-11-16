using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using sirrius.FTPFileWorker;
using sirrius.FTPFileWorker.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.CoreAPI
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
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:6000");
                })
        .ConfigureServices((hostContext, services) =>
        {
            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionJobFactory();

                //// Create a "key" for the job
                //var jobKey = new JobKey("HelloWorldJob");

                //// Register the job with the DI container
                //q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                //// Create a trigger for the job
                //q.AddTrigger(opts => opts
                //    .ForJob(jobKey) // link to the HelloWorldJob
                //    .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                //    .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

                // Register the job, loading the schedule from configuration

                q.AddJobAndTrigger<FileTransferJob>(hostContext.Configuration);
            });

            // Add the Quartz.NET hosted service
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // other config
        });
    }
}
