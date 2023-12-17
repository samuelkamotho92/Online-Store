using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Online_Store.Data;
using Online_Store.Extensions;
using Online_Store.Models;
using Online_Store.Services;
using Online_Store.Services.IService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//service using injection
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IOrder,OrderService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJWT, LoginService>();


builder.AddSwaggenGenExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



//Setting connection to database
builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});
builder.AddAuth();
//policy
builder.AddAdminPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//add middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
