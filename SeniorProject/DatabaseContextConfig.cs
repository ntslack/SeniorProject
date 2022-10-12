using Microsoft.EntityFrameworkCore;
using SeniorProject.Models.Context;

namespace SeniorProject
{
    public static class DatabaseContextConfig
    {
        public static void AddDatabaseContextConfig(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilderAction)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilderAction);
        }
    }
}
