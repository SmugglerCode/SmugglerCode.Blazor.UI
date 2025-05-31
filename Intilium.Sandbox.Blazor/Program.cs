using Intilium.Sandbox.Blazor.Components;
using Intilium.Sandbox.Blazor.Components.UI.Snackbar;
using Intilium.Sandbox.Blazor.Database;
using Intilium.Sandbox.Blazor.Database.CodeGen.Repositories;
using Intilium.Sandbox.Blazor.Database.Doc.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });

// Database registration.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CodeGenDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITypeClassRepository, TypeClassRepository>();
builder.Services.AddScoped<ITypeClassPropertyRepository, TypeClassPropertyRepository>();
builder.Services.AddScoped<IDiagramRepository, DiagramRepository>();
builder.Services.AddSingleton<IntiSnackbarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
