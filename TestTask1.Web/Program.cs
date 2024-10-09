using System.Reflection;
using TestTask1.Business.Repositories;
using TestTask1.DataAccess.Common.Repositories;
using TestTask1.DataAccess.Database.Configuration;
using TestTask1.DataAccess.Database.Repositories;
using TestTask1.Web.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ContractProfile)));

builder.Services.AddSingleton<DatabaseSettings>(_ => new DatabaseSettings(builder.Configuration.GetConnectionString("Default")!));
builder.Services.AddSingleton<IEmployeeRepository, EmployeeDatabaseRepository>();
builder.Services.AddSingleton<IPositionRepository, PositionDatabaseRepository>();
builder.Services.AddSingleton<IDepartmentRepository, DepartmentDatabaseRepository>();
builder.Services.AddSingleton<ICompanyInfoRepository, CompanyInfoCommonRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run("http://0.0.0.0:80");