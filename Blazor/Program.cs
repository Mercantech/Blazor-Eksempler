using Blazor.Components;
using dymaptic.GeoBlazor.Core;
using Microsoft.AspNetCore.StaticFiles;
using Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddBlazorBootstrap();
            builder.Services.AddGeoBlazor(builder.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSignalR();

            var app = builder.Build();
            app.MapHub<ChatHub>("/chathub");

            //GeoBlazor
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".wsv"] = "application/octet-stream";
            #if RELEASE
            provider.Mappings.Remove(".map");
            #endif
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });


            // Configure the HTTP request pipeline.

            app.UseExceptionHandler("/Error");
            app.UseHsts();


            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
