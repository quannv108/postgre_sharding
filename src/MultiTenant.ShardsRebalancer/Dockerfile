FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MultiTenant.ShardsRebalancer/MultiTenant.ShardsRebalancer.csproj", "src/MultiTenant.ShardsRebalancer/"]
RUN dotnet restore "src/MultiTenant.ShardsRebalancer/MultiTenant.ShardsRebalancer.csproj"
COPY . .
WORKDIR "/src/src/MultiTenant.ShardsRebalancer"
RUN dotnet build "MultiTenant.ShardsRebalancer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MultiTenant.ShardsRebalancer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiTenant.ShardsRebalancer.dll"]
