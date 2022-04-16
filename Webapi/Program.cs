using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Webapi.DBOperations;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Oluşturulan inmemoryDb nin projeye bağlanması.
builder.Services.AddDbContext<BookStoreDBContext>(
    options=>options.UseInMemoryDatabase(databaseName:"BookStoreDB")
);
// Auto Mapper Ayarı
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// app her çalıştığında DataGenerator başlatılsın.
using (var scope = app.Services.CreateScope()){
var services = scope.ServiceProvider;
DataGenerator.Inıtialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
