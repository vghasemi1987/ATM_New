using Autofac;
using DomainContracts.Commons;
using Infrastructure.Data.TransactionFileAggregate;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Extensions;
using Web.Extensions.AutofacServiceResolver;

namespace Web
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public Startup(IConfiguration configuration,
			 IWebHostEnvironment webHostEnvironment)
		{
			_configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpContextAccessor()
				.AddCustomizedDataStore(_configuration)
				.AddCustomizedIdentity()
				.AddCustomControllers()
				.AddCustomizedCookie(_webHostEnvironment)
				.AddCustomizedSession(_webHostEnvironment)
				.Build(_webHostEnvironment)
				.ConvertUrlsToLowercase()
				.AddResponseCompression()
				.AddRazorViewEngine()
				.AddScoped<IFileService, FileService>();

			services.AddScoped<TransactionFileRepository>();
		}

		public static void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new WebHandlerModule());
			//builder.RegisterModule(new SolutionHandlerModule());
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				//app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Catalog/Error");
				app.UseStatusCodePagesWithRedirects("~/error/code/{0}");
				app.UseHttpsRedirection();
				app.UseHsts();
			}

			//Acsess Angular API
			app.UseCors("Cors");

			//app.UseCustomizedHeader();
			app.UseCustomizedRequestLocalization();
			app.UseCustomizedStaticFiles(env);
			app.UseCookiePolicy();
			app.CustomExceptionMiddleware();
			app.UseSession();
			app.UseCustomizedMvc();
			app.UseCustomizedResponseCompression();
		}
	}
}