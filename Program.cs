using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AJP.ElasticBand;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using elasticband.ui.Services;
using MatBlazor;

namespace elasticband.ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.Services.AddElasticBand();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<IClipboardService, ClipboardService>();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 98;
                config.VisibleStateDuration = 3000;
            });

            builder.RootComponents.Add<App>("app");
            
            var host = builder.Build();
            
            var elasticBand = host.Services.GetService<IElasticBand>();
            var localStorageService = host.Services.GetService<ILocalStorageService>();

            var elasticSearchUrl = await localStorageService.GetItemAsync<string>("elasticSearchUrl");
            if (string.IsNullOrEmpty(elasticSearchUrl))
                elasticSearchUrl = "http://localhost:9200";

            var elasticSearchApiKey = await localStorageService.GetItemAsync<string>("elasticSearchApiKey");

            if(!string.IsNullOrEmpty(elasticSearchUrl))
                elasticBand.SetElasticsearchUrl(elasticSearchUrl);
            
            if(!string.IsNullOrEmpty(elasticSearchApiKey))
                elasticBand.SetElasticsearchAuthentication(elasticSearchApiKey);

            var jsRuntime = host.Services.GetService<IJSRuntime>();
            await jsRuntime.InvokeVoidAsync("monacoEditorFunctions.load"); // do this once rather than every page

            await host.RunAsync();
        }
    }
}
