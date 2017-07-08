using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AjentiMobile
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
			//services.Configure<IISOptions>(options => {
			//	options.AutomaticAuthentication = true;
			//});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				loggerFactory.AddFile($@"D:\ADMS\Logs\Mobile${DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt", append: true);
			}
			//var logger = loggerFactory.CreateLogger("Startup");
			//logger.LogDebug($"env.EnvironmentName = ${env.EnvironmentName}", env.EnvironmentName);

			app.Use(async (context, next) =>
			{
				await next();

				if (context.Response.StatusCode == 404 &&
					!Path.HasExtension(context.Request.Path.Value) &&
					!context.Request.Path.Value.StartsWith("/api/"))
				{
					context.Request.Path = "/index.html";
					await next();
				}
			});

			//app.UseRuntimeInfoPage();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();

			if (env.EnvironmentName != "Development")
			{
				var options = new RewriteOptions().AddRedirectToHttpsPermanent();
				app.UseRewriter(options);
			}

		}
	}
}
