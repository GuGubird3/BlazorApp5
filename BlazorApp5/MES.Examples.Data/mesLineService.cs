using BlazorApp5.ApiServers;
using BlazorApp5.MES.Examples.Data.Models;
using Newtonsoft.Json;
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

        public Task<IEnumerable<LineItem>> GetLineItems()
        {
            // 1. 模拟从 HTTP URL 获取 JSON 数据
            string jsonString = @"
            {
              ""LineItemGroups"": [
                {
                  ""LineItems"": [
                    {
                      ""Customer"": ""CustomerA"",
                      ""lineName"": ""Line1"",
                      ""partClass"": ""ClassX"",
                      ""csfileName"": ""FileA.cs""
                    },
                    {
                      ""Customer"": ""CustomerA"",
                      ""lineName"": ""Line2"",
                      ""partClass"": ""ClassY"",
                      ""csfileName"": ""FileB.cs""
                    }
                  ]
                },
                {
                  ""LineItems"": [
                    {
                      ""Customer"": ""CustomerB"",
                      ""lineName"": ""Line3"",
                      ""partClass"": ""ClassZ"",
                      ""csfileName"": ""FileC.cs""
                    },
                    {
                      ""Customer"": ""CustomerB"",
                      ""lineName"": ""Line4"",
                      ""partClass"": ""ClassX"",
                      ""csfileName"": ""FileD.cs""
                    }
                  ]
                }
              ]
            }";


            // 2. 反序列化 JSON
            lineTable? table = JsonConvert.DeserializeObject<lineTable>(jsonString);


            if (table == null || table.LineItemGroups == null)
            {
                // 如果反序列化失败或没有 LineItemGroups，返回空集合
                return Task.FromResult(Enumerable.Empty<LineItem>());
            }
            // 3. 提取所有 LineItems（扁平化处理）
            IEnumerable<LineItem> allLineItems = table?.LineItemGroups?
                .SelectMany(group => group.LineItems ?? Enumerable.Empty<LineItem>())
                ?? Enumerable.Empty<LineItem>();
            Console.WriteLine($"[INFO] 总共提取到 {allLineItems.Count()} 个LineItem");
            foreach (var item in allLineItems.Take(3)) // 只打印前3条示例
            {
                Console.WriteLine($"示例LineItem: 客户={item.Customer}, 产线={item.lineName},产品族={item.partClass}, 脚本={item.csfileName}");
            }
            // 4. 包装为 Task 并返回
            return Task.FromResult(allLineItems);

        }
    }
}
