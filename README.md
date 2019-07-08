# SimpleDiscovery

HttpClientFactory based client service discovery for .NET Core

[![Build Status](https://dev.azure.com/shibayan/SimpleDiscovery/_apis/build/status/Build%20SimpleDiscovery?branchName=master)](https://dev.azure.com/shibayan/SimpleDiscovery/_build/latest?definitionId=19&branchName=master)

## NuGet Packages

Package Name | Target Framework | NuGet
---|---|---
SimpleDiscovery | .NET Standard 2.0 | [![NuGet](https://img.shields.io/nuget/v/SimpleDiscovery.svg)](https://www.nuget.org/packages/SimpleDiscovery)
SimpleDiscovery.AzureAppConfiguration | .NET Standard 2.0 | [![NuGet](https://img.shields.io/nuget/v/SimpleDiscovery.AzureAppConfiguration.svg)](https://www.nuget.org/packages/SimpleDiscovery.AzureAppConfiguration)
SimpleDiscovery.EnvironmentVariables | .NET Standard 2.0 | [![NuGet](https://img.shields.io/nuget/v/SimpleDiscovery.EnvironmentVariables.svg)](https://www.nuget.org/packages/SimpleDiscovery.EnvironmentVariables)

## Basic usage

### 1. Deploy Azure App Configuration

https://docs.microsoft.com/en-us/azure/azure-app-configuration/

The key name is saved as `Registry:<service name>`. The value sets an endpoint, such as a REST API.

![image](https://user-images.githubusercontent.com/1356444/60800097-838e4a80-a1af-11e9-974c-bddd40af3a03.png)

### 2. Install nuget package

```
# Test use only for Azure Functions
Install-Package SimpleDiscovery.EnvironmentVariables -Pre

# Recommend
Install-Package SimpleDiscovery.AzureAppConfiguration -Pre
```

### 3. Setup `SimpleDiscovery`

#### Adding AppConfiguration connection string

```
dotnet user-secrets set "ConnectionStrings:AppConfig" "__AzureAppConfiguration_ConnectionString__"
```

#### Register `SimpleDiscovery` to DI

```csharp
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSimpleDiscovery()
                .AddAzureAppConfiguration(Configuration.GetConnectionString("AppConfig"));

        // Match the service name registered in App Configuration
        services.AddHttpClient("BuchizoService")
                .WithSimpleDiscovery();

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }
}
```

### 4. Using HttpClientFactory

```csharp
public class BuchizoController : Controller
{
    public BuchizoController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private readonly IHttpClientFactory _httpClientFactory;

    public async Task<IActionResult> Index()
    {
        var httpClient = _httpClientFactory.CreateClient("BuchizoService");

        // SimpleDiscovery automatically resolves destination host
        var response = await httpClient.GetStringAsync("/");

        return Content(response);
    }
}
```

## License

This project is licensed under the [MIT License](https://github.com/shibayan/SimpleDiscovery/blob/master/LICENSE)
