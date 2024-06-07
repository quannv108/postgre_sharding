FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MultiTenant.CoordinateSetup/MultiTenant.CoordinateSetup.csproj", "src/MultiTenant.CoordinateSetup/"]
RUN dotnet restore "src/MultiTenant.CoordinateSetup/MultiTenant.CoordinateSetup.csproj"
COPY . .
WORKDIR "/src/src/MultiTenant.CoordinateSetup"
RUN dotnet build "MultiTenant.CoordinateSetup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MultiTenant.CoordinateSetup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiTenant.CoordinateSetup.dll"]
