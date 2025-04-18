#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TechFood.Api/TechFood.Api.csproj", "src/TechFood.Api/"]
COPY ["src/TechFood.Application/TechFood.Application.csproj", "src/TechFood.Application/"]
COPY ["src/TechFood.Domain/TechFood.Domain.csproj", "src/TechFood.Domain/"]
COPY ["src/TechFood.Domain.Shared/TechFood.Domain.Shared.csproj", "src/TechFood.Domain.Shared/"]
COPY ["src/TechFood.Infra.Data/TechFood.Infra.Data.csproj", "src/TechFood.Infra.Data/"]
RUN dotnet restore "./src/TechFood.Api/./TechFood.Api.csproj"
COPY . .
WORKDIR "/src/src/TechFood.Api"
RUN dotnet build "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechFood.Api.dll"]