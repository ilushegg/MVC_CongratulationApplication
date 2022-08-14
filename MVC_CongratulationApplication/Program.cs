using Microsoft.EntityFrameworkCore;
using MVC_CongratulationApplication.DAL.Data;
using MVC_CongratulationApplication.DAL.Interface;
using MVC_CongratulationApplication.DAL.Repository;
using MVC_CongratulationApplication.Service.Interface;
using MVC_CongratulationApplication.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContainer, Container>();
builder.Services.AddScoped<ISendService, SendService>();
builder.Services.AddScoped<ITimedHostedService, TimedHostedService>();

builder.Services.AddHostedService<TimedHostedService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Activate}/{code?}");

app.Run();
