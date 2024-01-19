using BookStore.Models;
using BookStore.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SqlCon") ?? throw new InvalidOperationException("Connection string 'SqlCon' not found.");
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc();
            //builder.Services.AddSingleton<IBookStoreRepository<Book>, BookRepository>();
            //builder.Services.AddSingleton<IBookStoreRepository<Author>, AuthorRepository>();
            builder.Services.AddScoped<IBookStoreRepository<Book>, BookDBRepository>();
            builder.Services.AddScoped<IBookStoreRepository<Author>, AuthorDBRepository>();
           
            builder.Services.AddDbContext<BookStoreDBContext>(options =>options.UseSqlServer(connectionString));
           
            var app = builder.Build();

            //Migrations
            RunMigrations(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
        private static void RunMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookStoreDBContext>();
                db.Database.Migrate();
            }
        }
    }
}