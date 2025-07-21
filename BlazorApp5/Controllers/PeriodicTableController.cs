using BlazorApp5.MES.Examples.Data;
using BlazorApp5.MES.Examples.Data.Models;
using BlazorApp5.Servers;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Examples.Data;
using MudBlazor.Examples.Data.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;



[ApiController]
[Route("webapi/[controller]")]  // 访问路径：/webapi/periodictable

public class testController : ControllerBase
{
    private readonly ItestService _testService;

    public testController(ItestService testService)
    {
        _testService = testService;
    }


    [HttpGet]

    public async Task<ActionResult<string>> GetTestString()
    {
        try
        {
            var result = await _testService.GetTestStringAsync();
            return Ok(result); // 返回 HTTP 200 和结果
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}


[Route("webapi/[controller]")]  // 访问路径：/webapi/periodictable
public class periodictableController : ControllerBase
{
    private readonly IPeriodicTableService _periodicTableService;

    public periodictableController(IPeriodicTableService periodicTableService)
    {
        _periodicTableService = periodicTableService;
    }



    [HttpGet]
    public async Task<ActionResult<List<Element>>> GetElements()
    {
        try
        {
            var elements = await _periodicTableService.GetElements();
            Console.WriteLine($"===> 从服务获取的元素数量: {elements.Count()}");
            return Ok(elements.ToList()); // 转换为 List<Element> 并返回 HTTP 200
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Element>>> SearchElements(string search)
    {
        try
        {
            var elements = await _periodicTableService.GetElements(search);
            return Ok(elements.ToList()); // 转换为 List<Element> 并返回 HTTP 200
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

[Route("webapi/[controller]")]  // 访问路径：/webapi/periodictable
public class linetableController : ControllerBase
{
    private readonly ImesLineService _imesLineService;

    public linetableController(ImesLineService mesLineService)
    {
        _imesLineService = mesLineService;
    }


    [HttpPost("data")]
    public async Task<ActionResult<List<LineItem>>> GetElements()
    {
        try
        {
            // 1. 模拟从 HTTP URL 获取 JSON 数据
            string jsonString = @"
                                    [
                                      {
                                        ""LineItems"": [
                                          ""string1"",
                                          ""string2"",
                                          ""string3"",
                                          ""string4""
                                        ]
                                      },
                                      {
                                        ""LineItems"": [
                                          ""string5"",
                                          ""string6"",
                                          ""string7"",
                                          ""string8""
                                        ]
                                      }
                                    ]";

            // 2. 反序列化 JSON 数据
            List<LineItem>? data = JsonSerializer.Deserialize<List<LineItem>>(jsonString);

            if (data == null || !data.Any())
            {
                return BadRequest("数据不能为空");
            }

            // 在这里处理接收到的数据
            Console.WriteLine($"===> 接收到的元素数量: {data.Count()}");

            foreach (var LineItemGroup in data)
            {
                if (LineItemGroup.LineItems != null)
                {
                    Console.WriteLine($"元素值: {string.Join(", ", LineItemGroup.LineItems)}");
                }
            }

            return Ok(data.ToList()); // 转换为 List<Element> 并返回 HTTP 200
        }
        catch (JsonException ex)
        {
            return StatusCode(500, $"JSON 反序列化错误: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}



