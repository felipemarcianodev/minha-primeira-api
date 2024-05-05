namespace MinhaPrimeiraAPI
{
    public class Startup
    {
        public Startup(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            WebHostEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
            if(WebHostEnvironment.IsProduction()) 
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                       .ClearProviders()
                       .AddConsole()
                       .AddDebug()
                       .AddEventLog();
            });

            services.AddAuthorization();
        }
    }
}
