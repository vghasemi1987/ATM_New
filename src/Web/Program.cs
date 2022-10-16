using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //StiLicense.Key =
            //    "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7rE7BAeUEkpkjUKhl6/j24S6yxsIWZIRjJksEoLVUjBueVKUbrngXOqKSPJ8HE3n1pShqAKcqrYW8MlF8pB4nnRnYzLWJ/P+/p8zFGywvfSWm7L6hGvJFWozdlx5wLTj4K5UuclS2XfPNkIDrt7BY5X2KVdt42NBLZbM5RdUB8iJFobpp0HzoKZI8TSn++9s0y2cM/uGn0zHRcz/b8P/PiiOJkRkm0XlFrXG19KuA6eBAUfWiHYAgTMZq2UCyOdCbDZEcF8SqCGjboFuTyI7OHTQ4PVFQY8uEmsqhes9jqiz7u7Ts7Ndy88rVAe10GiHrBdyAGf4AR4G9DFrA10fnTGIVLixX8GpNTGgsLFIOf+IQOUvdcV39PeCf2JA2vEhSqbiaiftgGwxxgbc8ENPXijj+wYztDzMBeTJUwZBheNLcD2Rqwrc//HYvbuG6aZSjPCA5DvD3QJMvdBdHM3HWvlyU0tN6xVAiECAvWQdSOks";
            CreateHostBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddSerilog())
                .Build()
                .Run();

            //host.Run();
        }

        private static void SetupConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder configBuilder)
        {
            var env = hostingContext.HostingEnvironment;
            configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            configBuilder.AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}