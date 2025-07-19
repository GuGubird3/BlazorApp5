using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp5.Servers
{
    public class testService: ItestService
    {
        public async Task<string> GetTestStringAsync()
        {
            await Task.Delay(100); // 模拟异步操作
            return "Hello from testService!";
        }
    }
}
