using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using FamilyTreeWebAPI.Infrastructure.Data;
using FamilyTreeWebAPI.Application.Services;
using FamilyTreeWebAPI.Core.Interfaces;
using FamilyTreeWebAPI.Infrastructure.Data.Repositories;
using FamilyTreeWebAPI.Application.Mappings;
var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<FamilyTreeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddControllersWithViews()
//    .AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = null;
    options.JsonSerializerOptions.MaxDepth = 32; // You can adjust this value if needed
});
// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IFamilyTreeRepository, FamilyTreeRepository>();
builder.Services.AddScoped<IFamilyTreeService, FamilyTreeService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<FamilyTreeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FamilyTreeConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// Use CORS
app.UseCors("AllowAllOrigins");
app.Run();