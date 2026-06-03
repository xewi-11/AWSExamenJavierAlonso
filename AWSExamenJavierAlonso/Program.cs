using Amazon.S3;
using AWSExamenJavierAlonso.Data;
using AWSExamenJavierAlonso.Helpers;
using AWSExamenJavierAlonso.Models;
using AWSExamenJavierAlonso.Repositories;
using AWSExamenJavierAlonso.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

string misecret = await HelperSecretManager.GetSecretAsync();

//mapeamos el string con nuestro model

KeysModel model = JsonConvert.DeserializeObject<KeysModel>(misecret);
var provider = new ServiceCollection()
    .AddSingleton<KeysModel>(x => model)
    .BuildServiceProvider();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ZapatillasContext>(options =>
    options.UseMySQL(model.AwsBD));
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceStorageS3>();
builder.Services.AddTransient<RepositoryZapatillas>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
