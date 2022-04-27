using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Webapi.DBOperations;
using Webapi.Middlewares;
using Webapi.Services;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Authantication Ekliyoruz en başta yapılması gerekiyor.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    // Tokenın nasıl valide edileceğinin paremetrelerini veriyoruz.
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        // Benim token'ımı kimler kullana bilir.Client'ım kimlerdir.
        ValidateAudience = true,
        // Token'ın sağlayıcısı, dağıtıcısı kimlerdir.
        ValidateIssuer = true,
        // Token LifeTime'ını kontrol et. Lifetime tamamlandıysa token expire olsun ve yetkilendirmeyi kapat.
        ValidateLifetime = true,
        // Token'ı kriptolayacağımız, imzalayacağımız key anahtar.
        ValidateIssuerSigningKey = true,
        // Tokenın yaratılırken ki Issuer 'ı aşşağıdaki Issuer'dır.
        ValidIssuer = builder.Configuration["Token:Issuer"],
        // Tokenın Audience 'ı aşşağıdaki configurationdan gelecek.
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        // Tokenen üretildiği sunucunun time zone ile token'ı kullanacak olan client'ların time zone'ı farklı olduğunda token'ın adil bir şekilde dağıtılmasını sağlar.
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
// Oluşturulan inmemoryDb nin projeye bağlanması.
builder.Services.AddDbContext<BookStoreDBContext>(
    options => options.UseInMemoryDatabase(databaseName: "BookStoreDB")
);
// Interface olarak tanımladığımız yapıyı inject ediyoruz. Böylelikle ilerde hızlıca değişim gerçekleşebilecek.
builder.Services.AddScoped<IBookStoreDBContext>(provider => provider.GetService<BookStoreDBContext>());
// Auto Mapper Ayarı
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Oluşturduğumuz Loglama service'i ni DI container'a tanıtıyoruz. Sonra middlewarede kullanıcaz.
// Başka bir gün bize gelip deselerki DBLogger' olacak tek yapmam gereken consoleLogger yerine DBLogger yazmak.
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();


var app = builder.Build();

// app her çalıştığında DataGenerator başlatılsın.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Inıtialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Authentication olmadan Authorization olmak mümkün değil bu sıra çok önemli.
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

// Yazdığımız custom middleware'i endpointler çalışmadan önce bu kısımda kullanıcaz.
app.UseCustomExceptionMiddleware();

app.MapControllers(); // Bu UseEndpoints yerine gelen yapı daha doğrusu ismi değişti neyse.

app.Run();
