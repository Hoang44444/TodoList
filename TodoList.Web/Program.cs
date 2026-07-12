using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoList.Web;
using TodoList.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient trỏ tới API backend (KHÔNG phải origin của chính Blazor)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7118") });

// Service gọi API cho todo
builder.Services.AddScoped<ITodoApiService, TodoApiService>();

await builder.Build().RunAsync();
