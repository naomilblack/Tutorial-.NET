using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TutorialAPI.Models;

namespace TutorialAPI
{
    public class Startup
    {
        //this is an interface
        public IConfiguration Configuration {get;}

        //constructor expects for for the configuration object to be passed in
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //allows us to use the configuration interface for other things later.
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //register services here to set up and configure pipeline
        public void ConfigureServices(IServiceCollection services)
        {
            //tells the interface where to find the configuration for the database
            //configuration has to match the json exactly, including spacing.
            services.AddDbContext<CommandContext>
                (opt => opt.UseSqlServer(Configuration["Data:CommandAPIConnection:Connection String"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}

//migrations is a set of instructions that tells entity what we need to
//push to the database. They use command line prompts ex:
//dotnet ef migrations add <- tells entity to add a new command
