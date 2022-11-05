using sb_status;
using sb_status.Services;

using Yarp.ReverseProxy.LoadBalancing;
using Yarp.ReverseProxy.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddSingleton<ILoadBalancingPolicy, ActivePassiveLoadBalancingPolicy>();
builder.Services.AddSingleton<ClusterStatusIndicator>();
builder.Services.AddSingleton<IClusterChangeListener>(p => p.GetRequiredService<ClusterStatusIndicator>());
builder.Services.AddSingleton<IOfficialClusterStatus>(p => p.GetRequiredService<ClusterStatusIndicator>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapReverseProxy();

app.Run();