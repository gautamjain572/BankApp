using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataContext;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace BankApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // dependency injection
            services.AddScoped<IBankBL, BankBL>();
            services.AddScoped<IBankDataRL, BankDataRL>();

            //add db
            services.AddDbContext<BankDataCotext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
