using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Repository.AutomobilRepository;
using RentACarProject.Repository.ZaposleniRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<RentACarContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentACar"))
       .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IAutomobilRepository, AutomobilRepository>();
builder.Services.AddScoped<IZaposleniRepository, ZaposleniRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
