
using background_job_queue.Interfaces;
using background_job_queue.Services;

namespace background_job_queue
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Adicionar serviços ao contêiner
      builder.Services.AddCors(options =>
      {
        options.AddPolicy("PermitirTudo",
            builder =>
            {
                builder.WithOrigins("https://localhost:7115")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                  .AllowAnyHeader()
                  .AllowCredentials();
            });
      });

      // Add services to the container.
      builder.Services.AddControllers();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
      
      builder.Services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
      builder.Services.AddHostedService<QueuedHostedService>();
      builder.Services.AddSingleton<IFileStorage, InMemoryFileStorage>();
      builder.Services.AddSingleton<IExcelService, ExcelService>();
      builder.Services.AddSignalR();

      var app = builder.Build();
      app.MapHub<ExcelNotificationHub>("/excelNotificationHub");   


      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
        // Usar CORS
        app.UseCors("PermitirTudo");
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}