using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Common
{
    public class ContextManager
    {
        static public void CreateContext(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ShopContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
