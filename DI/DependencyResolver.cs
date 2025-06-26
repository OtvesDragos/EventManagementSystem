using BusinessLogic;
using BusinessLogic.Contracts;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Contracts;
using Repository.Contracts.Mappers;
using Repository.Mappers;
using Services;
using Services.Contracts;

namespace DI;
public static class DependencyResolver
{
    public static ServiceProvider ConfigServiceProvider(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddDbContext<DataContext>(options =>
            options.UseNpgsql(connectionString));

        service.AddScoped<IJwtTokenService, JwtTokenService>();
        service.AddScoped<IHashService, HashService>();

        service.AddScoped<IAuthBusinessLogic, AuthBusinessLogic>();
        service.AddScoped<IEventBusinessLogic, EventBusinessLogic>();
        service.AddScoped<IEventResponseBusinessLogic, EventResponseBusinessLogic>();
        service.AddScoped<IUserEventBusinessLogic, UserEventBusinessLogic>();

        service.AddScoped<IAuthRepository, AuthRepository>();
        service.AddScoped<IEventRepository, EventRepository>();
        service.AddScoped<IEventResponseRepository, EventResponseRepository>();
        service.AddScoped<IUserEventRepository, UserEventRepository>();

        service.AddScoped<IMapper<User, DataAccess.Entities.User>, UserMapper>();
        service.AddScoped<IMapper<Event, DataAccess.Entities.Event>, EventMapper>();
        service.AddScoped<IMapper<EventResponse, DataAccess.Entities.EventResponse>, EventResponseMapper>();

        return service.BuildServiceProvider();
    }
}

