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

The key name is saved as Services:`service name`. The value sets an endpoint, such as a REST API.

![image](https://user-images.githubusercontent.com/1356444/60766696-938f2700-a0e8-11e9-9734-6ab76ad5eb08.png)

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

        services.AddHttpClient("Buchizo")
                .WithSimpleDiscovery();

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }
}
```

## License

This project is licensed under the [MIT License](https://github.com/shibayan/SimpleDiscovery/blob/master/LICENSE)
