# DevOps 2023 - 2024 Web 

## Group

- Foodtruck Project
- A02 Group number

## Team members

### Development:

List of team members responsible for development:

- Kilian Ostijn | @Kilian3005
- Lander Van Molle | @LanderVM
- Sander Geuens | @SanderGeuens
- Thomas Lissens | @ThomasLissens
- Maurice Cantaert | @Skerath

### Operations:
 
| Name     | GitHub username                                         | Email address                        |
| :------- | :------------------------------------------------------ | :----------------------------------- |
| Jobbe    | [Niterfjord](https://github.com/niterfjord)             | jobbe.defeyter@student.hogent.be     |
| Pieter   | [Pieter-Deconinck](https://github.com/Pieter-Deconinck) | pieter.deconinck@student.hogent.be   |
| Metehan  | [MetehanAtili](https://github.com/MetehanAtili)         | metehan.atili@student.hogent.be      |
| Matthias | [mappelmans](https://github.com/mappelmans)             | matthias.appelmans@student.hogent.be |

## Technologies & Packages

[Fill-in] List of technologies and packages used in this project:

Technologies:
- Database: MariaDB
- .NET 7.0 with AspNetCore and Blazor WASM

 Packages:
- Back-end API documentation: Swagger
- Front-end component library: MudBlazor
- Front-end Google Maps api library: BlazorGoogleMaps 
- Validation: FluentValidation and Append.GuardClauses
- Database: EntityFramework Core with NewtonsoftJson (Converting lists to and from JSON to store in our db) and Pomelo.EntityFrameworkCore.MySql
- Storing and using images: Azure.Storage.Blobs
- Testing: NUnit with Playwright. XUnit for other tests. Using Shouldly

## Environment files

### Server\appsettings.development.json
```json
{
  "Auth0": {
    "Authority": "https://AUTHORITY_HERE.eu.auth0.com",
    "ApiIdentifier": " https://API_AUDIENCE_HERE"
  },
  "AllowedHosts": "*",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DBConnectionString": "server=SERVER_IP_HERE;user=USERNAME_HERE;password='PASSWORD_HERE';database=DATABASE_HERE",
    "Storage": "DefaultEndpointsProtocol=https;AccountName=ACCOUNT_NAME_HERE;AccountKey=ACCOUNT_KEY_HERE;EndpointSuffix=core.windows.net",
    "GoogleMaps": "MAPS_API_KEY_HERE"
  },
  "MailSettings": {
    "MailAdress": "user@email.tld",
    "Password": "password-password"
  }
}
```

### Client\wwwroot\appsettings.json
```json
{
  "Auth0": {
    "Authority": "https://AUTHORITY_HERE.eu.auth0.com",
    "ClientId": "CLIENT_ID_KEY_HERE",
    "Audience": " https://API_AUDIENCE_HERE"
  }
}
```

### Tests\PlaywrightTests\ClientTestCredentials.cs
```cs
namespace Client.PlaywrightTests;

public static class ClientTestCredentials
{
  public static string email = "USERNAME_HERE@EMAIL_DOMAIN_HERE.tld";
  public static string password = $"PASSWORD_HERE";
}
```
