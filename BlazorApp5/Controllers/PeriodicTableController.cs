using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MudBlazor.Examples.Data.Models;
using MudBlazor.Examples.Data;
using BlazorApp5.Servers;



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



