using BlazorApp5.ApiServers;
using BlazorApp5.Components;
using BlazorApp5.Servers;
using BlazorApp5.SQLServer.Data;
using BlazorApp5.SQLServer.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add MudBlazor services
builder.Services.AddMudServices(); // This includes IEventListenerFactory


builder.Services.AddSingleton<ItestService, testService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // �����κ���Դ����������Ӧ����Ϊ����������
              .AllowAnyMethod()    // �������� HTTP ������GET/POST/PUT �ȣ�
              .AllowAnyHeader();   // ������������ͷ
    });
});


builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7026"); // �滻Ϊʵ�� API ��ַ
});

builder.Services.AddSingleton<ApiService>();

builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});

// ????????
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ????????????
builder.Services.AddScoped<LineInfoRepository>();
builder.Services.AddScoped<LineItemService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAntiforgery();

// ����·��
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
