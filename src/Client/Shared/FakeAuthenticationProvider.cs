using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Project.Client.Shared;

public class FakeAuthenticationProvider : AuthenticationStateProvider
{
  public ClaimsPrincipal Current { get; private set; } = Anonymous;
  public static ClaimsPrincipal Anonymous => new(new ClaimsIdentity(new[]
      {
            new Claim(ClaimTypes.Name, "Anonymous"),
    }));
  public static ClaimsPrincipal Administrator =>
       new(new ClaimsIdentity(new[]
       {
     new Claim(ClaimTypes.Name, "Admin"),
     new Claim(ClaimTypes.Email, "fake-administrator@gmail.com"),
     new Claim(ClaimTypes.Role, "Administrator"),
       }, "Fake Authentication"));
  public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
  {
    Current = claimsPrincipal;
    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
  }
  public override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    return Task.FromResult(new AuthenticationState(Current));
  }
}
