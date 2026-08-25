
using BL.Settings;
using BL.Interfaces.Services;
using BL.Mappers;
using BL.Services;
using DAL.Data;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Domain.Activities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5077",
                    "https://localhost:5077",
                    "http://localhost:7018",
                    "https://localhost:7018"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddCors(option =>
                option.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:5077"))
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(
                        options => options.UseSqlServer(config.GetConnectionString("app")));

builder.Services.AddScoped<IDriverRepo, DriverRepo>();
builder.Services.AddScoped<IDriverService, DriverService>();

builder.Services.AddScoped<IVehicleRepo, VehicleRepo>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IMissionRepo, MissionRepo>();
builder.Services.AddScoped<IMissionService, MissionService>();

builder.Services.AddScoped<ISiteRepo, SiteRepo>();
builder.Services.AddScoped<ISiteService, SiteService>();

builder.Services.AddScoped<IVehicleAlertRepo, VehicleAlertRepo>();
builder.Services.AddScoped<IVehicleAlertService, VehicleAlertService>();

builder.Services.AddScoped<ITimesheetRepo, TimesheetRepo>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();

builder.Services.AddScoped<ITripAnomalyRepo, TripAnomalyRepo>();
builder.Services.AddScoped<ITripAnomalyService, TripAnomalyService>();

builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IInvoiceLineRepo, InvoiceLineRepo>();
builder.Services.AddScoped<IInvoiceLineService, InvoiceLineService>();

builder.Services.AddScoped<IPositionRepo, PositionRepo>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<IActivityRepo, ActivityRepo>();
builder.Services.AddScoped<IActivityService, ActivityService>();

builder.Services.AddSingleton(
    new ActivityReconstructionSettings
    {
        MaximumActivityDurationMilliseconds = null
    });

builder.Services.AddScoped<IActivityCodeMapper, ActivityCodeMapper>();

builder.Services.AddScoped<IActivityAnomalyDetector, ActivityAnomalyDetector>();

builder.Services.AddScoped<IActivityReconstructionService, ActivityReconstructionService>();

builder.Services.AddScoped<IActivityReconstructionService, ActivityReconstructionService>();

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(DriverProfile).Assembly
);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
