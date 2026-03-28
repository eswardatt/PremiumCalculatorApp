using premium_calculator_api.CustomMiddlerWare;
using premium_calculator_api.Services;
namespace premium_calculator_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<GlobalExceptionMiddleware>();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<IPremiumService, PremiumService>();

            builder.Services.AddCors(options =>{
             options.AddPolicy("PremiumCalculatorUI",
                 policy =>
                    {
                     policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod(); });});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
             // Enable CORS
            app.UseCors("PremiumCalculatorUI");
            // Global Exception Middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
