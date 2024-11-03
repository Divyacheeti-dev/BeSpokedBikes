using BeSpokedSalesModel.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesDbConnectionString")));
// Add services to the container.

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//builder.Services.AddControllers(o =>
//{
//    o.ReferenceHandler = ReferenceHandler.Preserve;
//    o.JsonSerializerOptions.MaxDepth = 0;
//});

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();