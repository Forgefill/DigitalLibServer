using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public class DalDIConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("MSSQLSERVER");
            services.AddDbContext<LibDbContext>(options => options.UseSqlServer(connection));
        }
    }
}
