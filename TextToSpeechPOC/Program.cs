using TextToSpeechPOC.Models;
using TextToSpeechPOC.Services;

namespace TextToSpeechPOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<GoogleCloudCredentials>(builder.Configuration.GetSection(nameof(GoogleCloudCredentials)));
            builder.Services.Configure<AzureCloudCredentials>(builder.Configuration.GetSection(nameof(AzureCloudCredentials)));

            builder.Services.AddScoped<ITextToSpeechService, AzureService>();
            //builder.Services.AddScoped<ITextToSpeechService, GoogleService>();
            // builder.Services.AddScoped<ITextToSpeechService, AmazonService>();
            //etc
            //you can create one Interface for each one if you want like
            //builder.Services.AddScoped<IAzureService, AzureService>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}