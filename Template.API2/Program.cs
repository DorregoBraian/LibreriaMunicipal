using Microsoft.EntityFrameworkCore;
using Template.AccessData2;
using Template.AccessData2.Commands;
using Template.Application2.Interfaces;
using Template.Application2.Services;
using Template.Domain2.Commands;

var MyCors = "_myCors";
var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
IConfiguration configuration = configBuilder.Build();
string connectionString = configuration.GetSection("ConnectionString").Value;

//Doy de alta los Controladores, el DbContext y AddAutoMapper
builder.Services.AddControllers();
builder.Services.AddDbContext<LibreriaDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program));

//Permisos Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCors,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

// Obtenga más información sobre cómo configurar Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle

//Soy de alta a los Endpoints que responderan a las petición de los Servidores. Ademas doy de alta al Swagger la cual es una erramienta de testin de API RESTful
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Doy de alta a todos los Servicios que necesito para utilizarlos
builder.Services.AddTransient<IClientesRepository, ClientesRepository>();
builder.Services.AddTransient<IClientesService, ClientesService>();
builder.Services.AddTransient<IAlquilerRepository, AlquilerRepository>();
builder.Services.AddTransient<IAlquilerService, AlquilerService>();
builder.Services.AddTransient<ILibrosRepository, LibrosRepository>();
builder.Services.AddTransient<ILibrosService, LibrosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyCors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();