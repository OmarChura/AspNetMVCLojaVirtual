using LojaVirtual.Database;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Interfaces;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Libraries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

/* padrao repository 
 */

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();

//session - configuracao
builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();  //guardar os dados na memoria
builder.Services.AddSession(options =>
{
   
});
builder.Services.AddScoped<Sessao>();
builder.Services.AddScoped<LoginCliente>();

string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

builder.Services.AddDbContext<LojaVirtualContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
