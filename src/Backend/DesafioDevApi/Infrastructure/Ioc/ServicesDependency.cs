using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Common;
using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Handlers;
using DesafioDevApi.Infrastructure.Data.Common;
using DesafioDevApi.Infrastructure.Data.Repositories;
using DesafioDevApi.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioDevApi.Infrastructure.Ioc
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddDbContext<ApiDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
               
            });
            services.AddDbContext<ApiDBContext>(delegate (DbContextOptionsBuilder options) { });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            #endregion
            #region Services
            services.AddScoped<ITransactionService, TransactionService>();
            #endregion
            #region Handlers
            services.AddMediatR(Assembly.GetEntryAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehaviour<,>));

            services
                .AddScoped<IRequestHandler<TransactionFileRequestCommand, Response>, TransactionFileHandler>()
                .AddScoped<IRequestHandler<TransactionGetAllRequestCommand, Response>, TransactionGetAllHandler>()
                .AddScoped<IRequestHandler<TransactionGetRequestCommand, Response>, TransactionGetHandler>();
            #endregion
            return services;
        }
    }
}
