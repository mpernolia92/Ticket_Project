using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Data;
using TicketOfUser.Models;
using TicketOfUser.Repository;
using TicketOfUser.Repository.IRepository;
using TicketOfUser.ViewModel;

namespace TicketOfUser
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
            services.AddDbContext<db_TicktingProgContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("contr")));
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSigninManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<db_TicktingProgContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSigninManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddDefaultTokenProviders();
            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();


            //services.AddScoped<ApplicationRoleStore>();
            //services.AddScoped<ApplicationUserStore>();

            //jwt

            var appSettingSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appSettingSection);

            var appSetting = appSettingSection.Get<AppSetting>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddCookie()
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicyisHere",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200/")
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                    });
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketOfUser", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketOfUser v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicyisHere");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"resources")),
                RequestPath = new PathString("/resources")
            });
            app.UseAuthentication();

            
            app.UseRouting();

            app.UseAuthorization();

            ////Data
            //IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.
            //    GetRequiredService<IServiceScopeFactory>();
            //using (IServiceScope scope = serviceScopeFactory.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService
            //        <RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService
            //        <UserManager<ApplicationUser>>();
            //    //Create Admin Role
            //    if (!await roleManager.RoleExistsAsync("Admin"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Admin";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create User Role
            //    if (!await roleManager.RoleExistsAsync("User"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "User";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Support Role
            //    if (!await roleManager.RoleExistsAsync("Support"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Support";
            //        await roleManager.CreateAsync(role);
            //    }
            //    //Create Developer Role
            //    if (!await roleManager.RoleExistsAsync("Developer"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Developer";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Admin User

            //    if (await userManager.FindByNameAsync("Admin") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "Admin";
            //        var userPassword = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Admin");
            //        }
            //    }

            //    //Create User

            //    if (await userManager.FindByNameAsync("User") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "User";

            //        var userPassword = "User@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "User");
            //        }
            //    }

            //    //Create Support

            //    if (await userManager.FindByNameAsync("Support") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "Support";

            //        var userPassword = "Support@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Support");
            //        }
            //    }

            //    //Create Developer

            //    if (await userManager.FindByNameAsync("Developer") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "Developer";

            //        var userPassword = "Developer@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Developer");
            //        }
            //    }
            //    if (await userManager.FindByNameAsync("Developer2") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "Developer2";

            //        var userPassword = "Developer@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Developer");
            //        }
            //    }
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
