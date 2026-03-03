using AutoShowcaseApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICarRepository, CsvCarRepository>();
builder.Services.AddScoped<IInquiryRepository, CsvInquiryRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

var dataPath = Path.Combine(app.Environment.ContentRootPath, "Data");
Directory.CreateDirectory(dataPath);
Directory.CreateDirectory(Path.Combine(app.Environment.WebRootPath, "images", "cars"));

var carsFile = Path.Combine(dataPath, "cars.csv");
var inquiriesFile = Path.Combine(dataPath, "inquiries.csv");

if (!File.Exists(carsFile))
    File.WriteAllText(carsFile, "Id,Make,Model,Year,Price,Color,Description,ImagePath,IsAvailable,DateAdded\n");

if (!File.Exists(inquiriesFile))
    File.WriteAllText(inquiriesFile, "Id,CarId,Name,Email,Phone,Message,DateSubmitted\n");

app.Run();
