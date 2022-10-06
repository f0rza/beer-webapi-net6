# Exercise: Beer rating 

Implementation done in .NET 6 using Swagger, Newtonsoft.JSON, Moq, RestSharp and async calls

#### Third-Party Plugins

* [`RestSharp`](https://www.nuget.org/packages/RestSharp)
* [`Newtonsoft.JSON`](https://www.nuget.org/packages/Newtonsoft.Json)
* [`Moq`](https://www.nuget.org/packages/Moq)
* [`Swagger`](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger)

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
   