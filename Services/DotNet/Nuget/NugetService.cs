﻿using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ice.Services.DotNet.Nuget
{
    public class NugetService
    {
        private string GetNugetFeedUrl(string query) => $"https://api-v2v3search-0.nuget.org/query?q={Uri.EscapeDataString(query)}";
        private string GetNugetHtmlUrl(string query) => $"https://www.nuget.org/packages/{Uri.EscapeDataString(query)}";
        
        public async Task<NugetResult> PerformNugetSearch(string query)
        {
            var request = HttpWebRequest.CreateHttp(GetNugetFeedUrl(query));

            return JsonConvert.DeserializeObject<NugetResult>(await new StreamReader((await request.GetResponseAsync()).GetResponseStream()).ReadToEndAsync());
        }
    }
}
