﻿using System;
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

            builder.RootComponents.Add<App>("app");
            
            var host = builder.Build();
            
            var elasticBand = host.Services.GetService<IElasticBand>();
            var localStorageService = host.Services.GetService<ILocalStorageService>();

            var elasticSearchUrl = await localStorageService.GetItemAsync<string>("elasticSearchUrl");
            if (string.IsNullOrEmpty(elasticSearchUrl))
                elasticSearchUrl = "http://localhost:9200";

            var elasticSearchApiKey = await localStorageService.GetItemAsync<string>("elasticSearchApiKey");

            elasticBand.SetElasticsearchUrl(elasticSearchUrl);
            elasticBand.SetElasticsearchAuthentication(elasticSearchApiKey);

            var jsRuntime = host.Services.GetService<IJSRuntime>();
            await jsRuntime.InvokeVoidAsync("monacoEditorFunctions.load"); // do this once rather than every page

            await host.RunAsync();
        }
    }
}
