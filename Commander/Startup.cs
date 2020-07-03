using System;
using AutoMapper;
using Commander.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace Commander
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
			// initialise le service de connexion à la bdd
			services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer(
				Configuration.GetConnectionString("CommanderConnection")
			));

			// ajout les controllers et la librairie json
			services.AddControllers().AddNewtonsoftJson(s =>
				s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
			);

			// initialise l'auto mapper
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			// ajoute une instance d'objet par client
			// le 2è arg permet de switcher facilement d'un repo à l'autre par dépendance d'injection (grâce à l'interface)
			services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
			//services.AddScoped<ICommanderRepo, MockCommanderRepo>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
