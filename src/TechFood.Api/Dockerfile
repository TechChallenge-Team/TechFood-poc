ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM ${DOTNET_SDK} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TechFood.Api/TechFood.Api.csproj", "src/TechFood.Api/"]
COPY ["src/TechFood.Application/TechFood.Application.csproj", "src/TechFood.Application/"]
COPY ["src/TechFood.Domain/TechFood.Domain.csproj", "src/TechFood.Domain/"]
COPY ["src/TechFood.Domain.Shared/TechFood.Domain.Shared.csproj", "src/TechFood.Domain.Shared/"]
COPY ["src/TechFood.Infra.Data/TechFood.Infra.Data.csproj", "src/TechFood.Infra.Data/"]
COPY ["src/TechFood.Infra.ImageStore.LocalDisk/TechFood.Infra.ImageStore.LocalDisk.csproj", "src/TechFood.Infra.ImageStore.LocalDisk/"]
COPY ["src/TechFood.Infra.Payments.MercadoPago/TechFood.Infra.Payments.MercadoPago.csproj", "src/TechFood.Infra.Payments.MercadoPago/"]
RUN dotnet restore "./src/TechFood.Api/./TechFood.Api.csproj"
COPY . .
WORKDIR /src/src/TechFood.Api
RUN dotnet build "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechFood.Api.dll"]
