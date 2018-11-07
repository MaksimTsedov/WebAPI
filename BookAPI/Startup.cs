namespace BookAPI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using BusinessLogic_BookAPI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Swagger;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class for launching configuration
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddSingleton<IBookShelf, LibraryObjectService>();
            services.AddSingleton<IGenreService, LibraryObjectService>();
            services.AddSingleton<IAuthorService, LibraryObjectService>();
            services.AddSingleton<ILibraryPairCreationManager, LibraryObjectService>();
            services.AddSingleton<ILibraryService, LibraryObjectService>();
            services.AddSingleton<IDataProvider, ObjectDataProvider>();*/       
            services.AddScoped<IBookShelf, LibraryDbService>();
            services.AddScoped<IGenreService, LibraryDbService>();
            services.AddScoped<IAuthorService, LibraryDbService>();
            services.AddScoped<ILibraryPairCreationManager, LibraryDbService>();
            services.AddScoped<ILibraryService, LibraryDbService>();
            services.AddScoped<IDataProvider, ObjectDataProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=(localdb)\mssqllocaldb;Initial Catalog=LibraryData;Integrated Security=True;";
            services.AddDbContext<LibraryDatabase>(options => options.UseSqlServer(connection, b=>b.MigrationsAssembly("BookAPI")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
