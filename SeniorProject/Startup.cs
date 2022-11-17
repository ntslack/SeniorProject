using SeniorProject.Models.Context;
using SeniorProject.Models.Services;
using SeniorProject.Models.Repositories;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace SeniorProject
{
    public class Startup
    {
        private readonly Action<DbContextOptionsBuilder> _optionsBuilder;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000/").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            AddDbContexts(services);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddTransient<IReminderService, ReminderService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskService, TaskService>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddTransient<IEventService, EventService>();

            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddTransient <IExpenseService, ExpenseService>();

            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddTransient<INoteService, NoteService>();

            services.AddScoped<IListRepository, ListRepository>();
            services.AddTransient<IListService, ListService>();

            services.AddScoped<IListItemRepository, ListItemRepository>();
            services.AddTransient<IListItemService, ListItemService>();

            services.AddScoped<IFavNotesRepository, FavNotesRepository>();
            services.AddTransient<IFavNotesService, FavNotesService>();

            services.AddScoped<IFavListsRepository, FavListsRepository>();
            services.AddTransient<IFavListsService, FavListsService>();

            services.AddScoped<IFavEventsRepository, FavEventsRepository>();
            services.AddTransient<IFavEventsService, FavEventsService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseSession();
            app.UseCors();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddDbContexts(IServiceCollection services)
        {
            Action<DbContextOptionsBuilder> optionsBuilderAction = (options) =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10), null);
                        });
        };
        services.AddDatabaseContextConfig(optionsBuilderAction);
        }
    }
}
