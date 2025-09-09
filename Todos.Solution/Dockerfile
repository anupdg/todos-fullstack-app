# Use the official .NET 9 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Use the .NET 9 SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution file
COPY ["Todos.Solution.sln", "."]

# Copy project files
COPY ["Todos.Api/src/Todos.Api.csproj", "Todos.Api/src/"]
COPY ["Todos.Api.Gql/Todos.Api.Gql.csproj", "Todos.Api.Gql/"]
COPY ["Todos.Data/src/Todos.Data.csproj", "Todos.Data/src/"]
COPY ["Todos.Services/Todos.Services.csproj", "Todos.Services/"]

# Restore dependencies
RUN dotnet restore "Todos.Api/src/Todos.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/Todos.Api/src"
RUN dotnet build "Todos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todos.Api.dll"]
