
using MediKlaudAPI6.Infrastructure;
using MediKlaudAPI6.Interface;
using MediKlaudAPI6.Interface.Cafeteria;
using MediKlaudAPI6.Interface.Pharmacy;
using MediKlaudAPI6.Service;
using MediKlaudAPI6.Service.Cafeteria;
using MediKlaudAPI6.Service.Pharmacy;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ICafeteriaService, CafeteriaCategoryService>();
builder.Services.AddTransient<IPhrPurchaseRequisitionService, PharPurchaseRequisitionService>();

builder.Services.AddSingleton<IMediklaudDBConnection, MediklaudDBConnection>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
       .WithOrigins("http://localhost:3000", "https://localhost:3000", "https://192.168.100.89:3000", "http://192.168.100.89:3000", "http://123.200.7.234:3000", "https://123.200.7.234:3000")
       .SetIsOriginAllowedToAllowWildcardSubdomains()
       .AllowAnyHeader()
       .AllowCredentials()
       .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
}

);
app.UseCors();


app.MapControllers();
app.Run();
