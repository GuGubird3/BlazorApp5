using BlazorApp5.MES.Examples.Data;
using BlazorApp5.MES.Examples.Data.Models;
using BlazorApp5.Servers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;




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
public class linetableController : ControllerBase
{
    private readonly ImesLineService _imesLineService;

    public linetableController(ImesLineService mesLineService)
    {
        _imesLineService = mesLineService;
    }


    [HttpGet]
    public async Task<ActionResult<List<LineItem>>> GetElements()
    {
        try
        {

            IEnumerable<LineItem> lineItems = await _imesLineService.GetLineItems();
            if (lineItems == null || !lineItems.Any())
            {
                return BadRequest("数据不能为空");
            }

            Console.WriteLine($"===> 接收到的元素数量: {lineItems.Count()}");

            return Ok(lineItems.ToList()); // 转为 List<LineItem>
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



