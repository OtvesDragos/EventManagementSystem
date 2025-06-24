using BusinessLogic;
using BusinessLogic.Contracts;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Contracts;

namespace DI;
public static class DependencyResolver
{
    public static ServiceProvider ConfigServiceProvider(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddDbContext<DataContext>(options =>
            options.UseNpgsql(connectionString));

        service.AddSingleton<IAuthBusinessLogic, AuthBusinessLogic>();
        service.AddSingleton<IAuthRepository, AuthRepository>();

        return service.BuildServiceProvider();
    }
}

