# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app v # Create default working directory
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src # Create default working directory
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"] # Copy the project file
RUN dotnet restore "./Services/Catalog/Catalog.API/Catalog.API.csproj" # Restore the project dependencies inside the container
# Reads dependencies from the .csproj.
# Downloads packages from NuGet into the Docker layer (typically under /root/.nuget/packages by default).
# Caches this layer in Docker, so you don’t re-download packages unless csproj changes.

COPY . . # Copy the rest of the source code
WORKDIR "/src/Services/Catalog/Catalog.API"
RUN dotnet build "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]