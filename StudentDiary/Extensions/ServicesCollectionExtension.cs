using StudentDiary.Data.Common;

namespace StudentDiary.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();


            return services;
        }
    }
}
