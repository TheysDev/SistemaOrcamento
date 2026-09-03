FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj", "OrcamentoSaaS.Api/"]
COPY ["OrcamentoSaaS.Shared/OrcamentoSaaS.Shared.csproj", "OrcamentoSaaS.Shared/"]
RUN dotnet restore "OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj"
COPY . .
WORKDIR "/src/OrcamentoSaaS.Api"
RUN dotnet build "./OrcamentoSaaS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrcamentoSaaS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrcamentoSaaS.Api.dll"]
