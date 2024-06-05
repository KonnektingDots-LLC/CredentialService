# Use the .NET 6 SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) and restore dependencies
COPY cred-system-back-end-app.csproj .
RUN dotnet restore

# Copy the entire project and build
COPY . .
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish -c Release -o out --no-restore

# Use the .NET 6 Runtime as the base image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Install curl
RUN apt-get update && apt-get install -y curl

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "cred-system-back-end-app.dll"]


