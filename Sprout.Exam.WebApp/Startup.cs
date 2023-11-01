using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sprout.Exam.Business.Core;
using Sprout.Exam.Business.Employee;
using Sprout.Exam.Business.Factory;
using Sprout.Exam.Business.Interface;
using Sprout.Exam.Business.Strategy;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Interface;
using Sprout.Exam.DataAccess.Repository;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Mapper;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Validator;
using System.Data;

namespace Sprout.Exam.WebApp
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			services.AddAuthentication()
				.AddIdentityServerJwt();

			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddSingleton<IEmployee, ContractualEmployee>();
			services.AddSingleton<IEmployee, RegularEmployee>();
			services.AddSingleton<EmployeeFactory>(_ =>
			{
				EmployeeFactory factory = new EmployeeFactory();
				factory.AddEmployee(EmployeeType.Contractual, new ContractualEmployee());
				factory.AddEmployee(EmployeeType.Regular, new RegularEmployee());
				return factory;
			});
			services.AddSingleton<EmployeeStrategy>();
			services.AddSingleton<EmployeeValidator>();
			services.AddSingleton<IEmployeeSalaryCalculator, EmployeeSalaryCalculator>();
			services.AddTransient<IEmployeeRepository, EmployeeRepository>();
			services.AddTransient<IDbConnection>((sp) => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

			//mapper
			var config = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			IMapper mapper = config.CreateMapper();
			services.AddSingleton(mapper);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
