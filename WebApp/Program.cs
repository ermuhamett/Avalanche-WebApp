using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� �������� ���� ������ � ����������� ��� �� ������������� PostgreSQL
builder.Services.AddDbContext<AvalancheDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//��������� ������� � DI
builder.Services.AddTransient<ExcelDataImporter>();
builder.Services.AddTransient<AvalancheDataService>();
builder.Services.AddTransient<RegionDataService>();

// Add services for the controller.
builder.Services.AddControllersWithViews();

// ��������� ��������� Razor Pages
builder.Services.AddRazorPages();


var app = builder.Build();
// ��������� �������� ��� ������� ����������
/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AvalancheDbContext>();
    dbContext.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    /*app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvalancheMonitoring v1"));*/
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ��������� Razor Pages
app.MapRazorPages();

app.Run();
