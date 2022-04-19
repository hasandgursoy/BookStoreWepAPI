using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Webapi.DBOperations;
using Webapi.Middlewares;
using Webapi.Services;
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
// Oluşturduğumuz Loglama service'i ni DI container'a tanıtıyoruz. Sonra middlewarede kullanıcaz.
// Başka bir gün bize gelip deselerki DBLogger' olacak tek yapmam gereken consoleLogger yerine DBLogger yazmak.
builder.Services.AddSingleton<ILoggerService,ConsoleLogger>();


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

// Yazdığımız custom middleware'i endpointler çalışmadan önce bu kısımda kullanıcaz.
app.UseCustomExceptionMiddleware();

app.MapControllers(); // Bu UseEndpoints yerine gelen yapı daha doğrusu ismi değişti neyse.


app.Run();
