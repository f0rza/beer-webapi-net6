# Exercise: Beer rating 

Implementation done in .NET 6 using Swagger, Newtonsoft.JSON, Moq, RestSharp and async calls.

## Prerequisites

Must have .NET 6 installed.

## Project start (command prompt)

Being in soluion folder use command prompt to execute the following commands:

```sh
1. dotnet restore 
2. dotnet msbuild
3. dotnet run --project Brewery.API
```

   Find in console output project root URL:
   ```text
      Now listening on: http://localhost:5091
   ```
   
   This means that API is up and running. You can use swagger interface to get access to the endpoints by adding **/swagger/index.html** to the url.
## Project start (Visual Studio)

Restore Nuget packages, build solution, set **Brewery.API** as startup project. Run (F5).

### Application Config

Configurations saved in `appsettings.json`

1. `ApiRoot` url to Punk API:

   ```json
     "ApiRoot": "https://api.punkapi.com/v2/", 
   ```

2. `LocalFile` filename of .json that is created or appended in API folder:

   ```json
   "ConnectionStrings": {
        "LocalFile": "database.json"
    }
   ```
### Log Configuration

NLog is used for application logging. Configuration can be adjusted in **nlog.config**. By default log files created in  `Brewery.API\bin\[CurrentEnvironment]\net6.0\logs\`

On controller level shown logging of errors and sample warning. 
On service level added several debug logs.
Repositories doesn't have logging as for now, but all unhandled exceptions are logged with a help of middleware.  

### Unit tests

Added several unit tests to demo MSTest / Moq.

### Third-Party Plugins
Project implementation relies on these dependencies:
* [`RestSharp`](https://www.nuget.org/packages/RestSharp)
* [`Newtonsoft.JSON`](https://www.nuget.org/packages/Newtonsoft.Json)
* [`NLog`](https://www.nuget.org/packages/NLog)
* [`Moq`](https://www.nuget.org/packages/Moq)
* [`Swagger`](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger)