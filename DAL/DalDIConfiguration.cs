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
            string connection = configuration.GetConnectionString("OracleLocal");
            services.AddDbContext<LibDbContext>(options => options.UseOracle(connection));
        }
    }
}
