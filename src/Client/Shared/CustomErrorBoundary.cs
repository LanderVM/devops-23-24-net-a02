using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace devops_23_24_net_a02.Client.Shared;

public class CustomErrorBoundary : ErrorBoundary
{
  [Inject] private IWebAssemblyHostEnvironment Environment { get; set; }

  protected override Task OnErrorAsync(Exception ex)
  {
    /*if (Environment.IsDevelopment())
    {
      return base.OnErrorAsync(ex);
    }*/
    return Task.CompletedTask;
  }
}
