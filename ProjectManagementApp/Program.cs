using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using MudBlazor.Services;
using ProjectManagementApp.SampleData;
using ProjectManagementApp.Services;
using ProjectManagementApp.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IProjectOwnerService,ProjectOwnerService>();
builder.Services.AddScoped<IProjectRoleService,ProjectRoleService>();
builder.Services.AddScoped<IProjectService,ProjectService>();
builder.Services.AddScoped<IStageService,StageService>();
builder.Services.AddScoped<IPhaseService,PhaseService>();
builder.Services.AddScoped<IPhaseOwnerService,PhaseOwnerService>();
builder.Services.AddScoped<IPhaseStageService,PhaseStageService>();
builder.Services.AddScoped<IPhaseAssignmentService,PhaseAssignmentService>();
builder.Services.AddScoped<IPhaseScheduleService,PhaseScheduleService>();

var connectionString = builder.Configuration.GetConnectionString("ProjectManagement") ?? throw new InvalidOperationException("Connection string 'ProjectManagement' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMudServices();

var apiUri = new Uri("http://localhost:5030");
builder.Services.AddHttpClient<ProjectHttpClient>(client => client.BaseAddress = apiUri);
builder.Services.AddHttpClient<PhaseHttpClient>(client => client.BaseAddress = apiUri);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//  SWAPPING TO SERVER FOR BACKEND
// Setup initial data for the projects 
//await app.BuildFakeProject();

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<ProjectManagementApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
