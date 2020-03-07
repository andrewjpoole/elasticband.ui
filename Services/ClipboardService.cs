using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticband.ui.Services
{
    public class ClipboardService : IClipboardService
    {
        private readonly IJSRuntime _jsRuntime;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> SetClipboard(string content) 
        {
            Console.WriteLine($"setClipboard calling");
            var response = await _jsRuntime.InvokeAsync<string>("clipboardFunctions.set", content);
            Console.WriteLine($"setClipboard returned {response}");
            return response == "true";
        }

        public async Task<string> GetClipboard() 
        {
            var content = await _jsRuntime.InvokeAsync<string>("clipboardFunctions.get");
            return content;
        }
    }
}
