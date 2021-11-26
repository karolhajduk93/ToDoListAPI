using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI
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
			services.Configure<ToDoListsDatabaseSettings>(
				Configuration.GetSection(nameof(ToDoListsDatabaseSettings)));

			services.AddSingleton<IToDoListsDatabaseSettings>(sp =>
				sp.GetRequiredService<IOptions<ToDoListsDatabaseSettings>>().Value);

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
					builder.WithOrigins("https://localhost:5001", "http://localhost:5000")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.WithMethods("GET, PATCH, DELETE, PUT, POST, OPTIONS"));
			});

			services.AddSingleton<ToDoListService>();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoListAPI", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoListAPI v1"));
			}
			app.UseCors(policy =>
				policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
					.AllowAnyMethod()
					.WithHeaders(HeaderNames.ContentType));

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
