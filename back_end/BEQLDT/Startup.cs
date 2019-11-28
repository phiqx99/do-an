using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEQLDT.Data;
using BEQLDT.Data.Infrastructure;
using BEQLDT.Data.Repositories;
using BEQLDT.Service;
using BEQLDT.Service.KeycloakService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BEQLDT
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc();
            services.AddCors();
            services.AddDbContext<FDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection"), opt => opt.UseRowNumberForPaging()));


            services.AddScoped<Config>();


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbFactory, DbFactory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupUserRepository, GroupUserRepository>();
            services.AddScoped<IGroupUserService, GroupUserService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ITopicCouncilRepository, TopicCouncilRepository>();
            services.AddScoped<ITopicCouncilService, TopicCouncilService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IPeriodRepository, PeriodRepository>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IDecisionRepository, DecisionRepository>();
            services.AddScoped<IDecisionService, DecisionService>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICouncilRepository, CouncilRepository>();
            services.AddScoped<ICouncilService, CouncilService>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = "https://localhost:44346",
                ValidIssuer = "https://localhost:44346",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkey")),

            };
        });


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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(builder =>
             builder.WithOrigins("*")
             .AllowAnyHeader()
             .AllowAnyMethod()
             );

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
