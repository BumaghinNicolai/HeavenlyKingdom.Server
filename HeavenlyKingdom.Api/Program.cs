using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.BusinessLogic.Services;
using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.DataAccess.Repositories;
using HeavenlyKingdom.Helpers.Mapping;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Репозитории (DAL)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFatherRepository, FatherRepository>();
builder.Services.AddScoped<ICandleRepository, CandleRepository>();

// Сервисы (BLL)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFatherService, FatherService>();
builder.Services.AddScoped<ICandleService, CandleService>();

// AutoMapper — MappingProfile лежит в HeavenlyKingdom.Helpers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// CORS для фронта (Vite по умолчанию на 5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Frontend");
app.UseSession();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();