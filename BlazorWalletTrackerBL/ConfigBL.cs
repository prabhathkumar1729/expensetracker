using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorWalletTrackerDAL.Models;
using BlazorWalletTrackerDAL.Repositories;

namespace BlazorWalletTrackerBL
{
    public class ConfigBL
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionRepo, TransactionRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddDbContext<ExpenseTrackerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("defaultconnection"))
            );
        }
    }
}