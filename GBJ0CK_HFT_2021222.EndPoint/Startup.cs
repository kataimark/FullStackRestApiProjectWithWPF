using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GBJ0CK_HFT_2021222.Logic;
using GBJ0CK_HFT_2021222.Models;
using GBJ0CK_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBJ0CK_HFT_2021222.Endpoint.Services;

namespace GBJ0CK_HFT_2021222.EndPoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILolPlayerLogic, LolPlayerLogic>();
            services.AddSingleton<ILolTeamLogic, LolTeamLogic>();
            services.AddSingleton<ILolManagerLogic, LolManagerLogic>();

            services.AddSingleton<IRepository<LolPlayer>, LolPlayerRepository>();
            services.AddSingleton<IRepository<LolTeam>, LolTeamRepository>();
            services.AddSingleton<IRepository<LolManager>, LolManagerRepository>();

            services.AddSingleton<DbContext, LolPlayerDbContext>();
            services.AddSignalR();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             app.UseCors(x => x
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:58709"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
