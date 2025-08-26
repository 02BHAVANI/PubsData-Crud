using Microsoft.EntityFrameworkCore;
using PubsData.Infrastructure.Data;
using PubsData.Infrastructure.Repositories;
using PubsData.Application.Interfaces;
using PubsData.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PubsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PubsConnection")));


builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<PublishersRepository>();
builder.Services.AddScoped<TitlesRepository>();
builder.Services.AddScoped<SalesRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<ISalesService, SalesService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Authors}/{action=Index}/{id?}");
app.Run();