using BlazorApp5.ApiServers;
using BlazorApp5.MES.Examples.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp5.MES.Examples.Data
{
    public class mesLineService: ImesLineService
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public mesLineService(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions;
        }

        public Task<IEnumerable<LineItem>> GetLineItems(string apiUrl)
        {

        }
    }
}
