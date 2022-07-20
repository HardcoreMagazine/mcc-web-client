using mcc_web_client.Data;

namespace mcc_web_client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            
            // Database context
            var connectionString = builder.Configuration
                .GetConnectionString("MySqlConnection");
            builder.Services.Add(new ServiceDescriptor(typeof(AppDbContext), 
                new AppDbContext(connectionString)));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}