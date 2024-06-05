# Introduction 
This ASP.Net Core API serves the resources needed by the credentialing web application. 

# External Dependencies
* Azure Function - The function is defined by another proyect.
* Azure Storage
* Azure B2C
* Azure SQL Server

# Prerequisites
* [ASP.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* External dependencies must be available and in reach by the API.

# Build and Test (Developer environment)
Add a `Secrets.json` file with the required secrets, for example:
```
{
  "ConnectionStrings:SqlServer": "<<SQL Server Connection String>>",
  "AzureStorageAccount:AzureBlobStorageKey": "DefaultEndpointsProtocol=https;AccountName=<<Account Name>>;AccountKey=<<Account key>>;EndpointSuffix=core.windows.net",
  "SmtpSettings:SmtpPass": "<<SMTP Password>>",
  "SmtpSettings:SenderEmail": "<<SMTP default sender email>>",
  "SmtpSettings:Username": "<<SMTP username>>",
  "FEUrl": "<<The public URL for the Web App>>",
  "AzureFunctionPdf": "<<The public URL for the Azure function>>",
}
```

To build the project run these commands from the CLI in the directory of the project.
```
dotnet build
dotnet run
```
These will install any needed dependencies, build the project, and run the project respectively.